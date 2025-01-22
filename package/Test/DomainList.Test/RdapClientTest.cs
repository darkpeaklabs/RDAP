using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DarkPeakLabs.PublicSuffix;
using DarkPeakLabs.Rdap;
using DarkPeakLabs.Rdap.Bootstrap;
using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.CorporateDomains.Rdap.Test;

[TestClass]
public class RdapClientTest
{
    private static readonly object _lock = new();

    [TestMethod]
    public void TestCiscoUmbrella()
    {
        CiscoUmbrellaDomainList domainList = new();
        PublicSuffixList publicSuffixList = new PublicSuffixList();
        HashSet<string> domains = [];
        foreach(var domain in domainList.GetDomainsAsync().GetAwaiter().GetResult())
        {
            try
            {
                domains.Add(publicSuffixList.GetDomainApex(domain).ToLowerInvariant());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting domain apex for {domain}: {ex.Message}");
            }
        }

        ConcurrentQueue<string> queue = new ConcurrentQueue<string>(domains);

        using StreamWriter writer = new StreamWriter($"test_result.csv", append: false);
        TestClientAsync(queue, writer).GetAwaiter().GetResult();
    }

    private static async Task TestClientAsync(ConcurrentQueue<string> queue, StreamWriter writer)
    {
        List<Task> tasks = [];

        int total = queue.Count;
        int count = 0;
        for(int i = 0; i < 20; i++)
        {
            tasks.Add(TestClientAsync(queue, writer, i, total, () => { Interlocked.Increment(ref count); return count; }));
        }

        await Task.WhenAll(tasks);
    }

    private static async Task TestClientAsync(ConcurrentQueue<string> queue, StreamWriter writer, int threadIndex, int total, Func<int> getCount)
    {
        RdapDnsBootstrap bootstrap = new RdapDnsBootstrap();
        using RdapClient client = new RdapClient();

        int count = 0;
        while(queue.TryDequeue(out string domain))
        {
            count++;
            try
            {
                var serviceUri = await bootstrap.FindServiceUrlAsync(domain);
                DebugWriteLine($"[{getCount.Invoke()}/{total}:{threadIndex}] Looking up domain {domain}, service: {serviceUri}");
                await LookupDomainAsync(client, serviceUri, domain, writer);
            }
            catch(Exception exception)
            {
                DebugWriteLine($"Error domain:{domain} - {exception.Message}");
                LogError(writer, null, domain, exception);
            }
        }
    }

    private static async Task LookupDomainAsync(
        RdapClient client,
        Uri serviceUri,
        string domain,
        StreamWriter writer)
    {
        var (success, result) = await TryExecuteLookupAsync(
            () => client.DomainLookupAsync(serviceUri, domain),
            serviceUri,
            domain,
            writer);

        if (success)
        {
            await ProcessDomainResponseAsync(client, result.Value, domain, [serviceUri.Host], writer);
        }
    }

    private static async Task<(bool Success, T Result)> TryExecuteLookupAsync<T>(
        Func<Task<T>> lookup,
        Uri serviceUri,
        string domain,
        StreamWriter writer) where T : class 
    {
        try
        {
            var response = await lookup.Invoke();
            return (true, response);
        }
        catch (RdapRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
        {
            DebugWriteLine($"serviceURI: {serviceUri}, domain:{domain} - NotFound");
        }
        catch (Exception exception)
        {
            DebugWriteLine($"serviceURI: {serviceUri}, domain:{domain} - {exception.Message}");
            LogError(writer, serviceUri, domain, exception);
        }
        return (false, null);
    }

    private static void DebugWriteLine(string message)
    {
        lock (_lock)
        {
            Debug.WriteLine(message);
        }
    }

    private static void LogError(StreamWriter writer, Uri serviceUri, string domain, Exception exception)
    {
        if (writer == null)
        {
            return;
        }

        string errorCode = exception switch
        {
            RdapRequestException requestException => requestException.StatusCode.HasValue ? requestException.StatusCode.Value.ToString() : "UnknownStatusCode",
            RdapBootstrapException bootstrapException => "Bootstrap",
            RdapJsonException jsonException => "Json",
            _ => exception.GetType().Name
        };

        lock (_lock)
        {
            writer.WriteLine($"{errorCode},{GetCsvValue(serviceUri)},{GetCsvValue(domain)},\"{exception.Message}\"");
        }
    }

    private static string GetCsvValue(object value)
    {
        if (value == null)
        {
            return string.Empty;
        }
        return $"\"{value}\"";
    }

    private static async Task LookupDomainAsync(
        RdapClient client,
        Uri uri,
        string domain,
        HashSet<string> hosts,
        StreamWriter writer)
    {
        if (hosts.Contains(uri.Host))
        {
            return;
        }

        hosts.Add(uri.Host);

        Debug.WriteLine($"Looking up domain {uri}");
        var (success, result) = await TryExecuteLookupAsync(
            () => client.DomainLookupAsync(uri),
            uri,
            null,
            writer);
        if (success)
        {
            await ProcessDomainResponseAsync(client, result.Value, domain, hosts, writer);
        }
    }

    private static async Task ProcessDomainResponseAsync(
        RdapClient client,
        RdapDomainLookupResponse response,
        string domain,
        HashSet<string> hosts,
        StreamWriter writer)
    {
        if (response.Links == null || response.Links.Count == 0)
        {
            return;
        }

        foreach (var link in response.Links.Where(x => x.Relation == RdapLinkRelationType.Related))
        {
            var serviceUrl = link.Value ?? link.Href;
            if (string.IsNullOrEmpty(serviceUrl))
            {
                continue;
            }
            await LookupDomainAsync(client, new Uri(serviceUrl), domain, hosts, writer);
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using DarkPeakLabs.PublicSuffix;
using DarkPeakLabs.Rdap.Bootstrap;
using DarkPeakLabs.Rdap.Values;

namespace DarkPeakLabs.Rdap.Utilities;

public class RdapClientTest
{
    private static readonly object _lock = new();
    private static readonly ConcurrentDictionary<string, RdapServiceLock> serviceLocks = new();
    private readonly string _resultPath;

    public RdapClientTest(string resultPath)
    {
        _resultPath = resultPath;
    }


    public async Task RunTestAsync()
    {
        CiscoUmbrellaDomainList domainList = new();
        PublicSuffixList publicSuffixList = new PublicSuffixList();
        HashSet<string> domains = [];
        foreach(var domain in domainList.GetDomainsAsync().GetAwaiter().GetResult())
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                domains.Add(publicSuffixList.GetDomainApex(domain).ToLowerInvariant());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting domain apex for {domain}: {ex.Message}");
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }

        ConcurrentQueue<string> queue = new ConcurrentQueue<string>(domains);

        using StreamWriter writer = new StreamWriter(Path.Combine(_resultPath, $"test_result.csv"), append: false);
        WriteCsvHeader(writer);
        await TestClientAsync(queue, writer).ConfigureAwait(false);
    }

    private async Task TestClientAsync(ConcurrentQueue<string> queue, StreamWriter writer)
    {
        List<Task> tasks = [];

        int total = queue.Count;
        int count = 0;
        for(int i = 0; i < 20; i++)
        {
            tasks.Add(TestClientAsync(queue, writer, i, total, () => { Interlocked.Increment(ref count); return count; }));
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    private async Task TestClientAsync(ConcurrentQueue<string> queue, StreamWriter writer, int threadIndex, int total, Func<int> getCount)
    {
        RdapDnsBootstrap bootstrap = new RdapDnsBootstrap();
        using RdapClient client = new RdapClient();

        int count = 0;
        while(queue.TryDequeue(out string? domain))
        {
            count++;
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                var serviceUri = await bootstrap.FindServiceUrlAsync(domain).ConfigureAwait(false);
                DebugWriteLine($"[{getCount.Invoke()}/{total}:{threadIndex}] Looking up domain {domain}, service: {serviceUri}");
                await LookupDomainAsync(client, serviceUri, domain, writer, threadIndex).ConfigureAwait(false);
            }
            catch(Exception exception)
            {
                DebugWriteLine($"Error domain:{domain} - {exception.Message}");
                LogError(writer, null, domain, exception);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }

    private async Task LookupDomainAsync(
        RdapClient client,
        Uri serviceUri,
        string domain,
        StreamWriter writer,
        int threadIndex)
    {
        var (success, result) = await TryExecuteLookupAsync(
            () => client.DomainLookupAsync(serviceUri, domain),
            serviceUri,
            domain,
            writer,
            threadIndex).ConfigureAwait(false);

        if (success && result != null)
        {
            await ProcessDomainResponseAsync(client, result.Value, domain, [serviceUri.Host.ToUpperInvariant()], writer, threadIndex).ConfigureAwait(false);
        }
    }

    private async Task<(bool Success, T? Result)> TryExecuteLookupAsync<T>(
        Func<Task<T>> lookup,
        Uri serviceUri,
        string domain,
        StreamWriter writer,
        int threadIndex) where T : class 
    {
        var serviceLock = serviceLocks.GetOrAdd(serviceUri.Host, new RdapServiceLock(serviceUri.Host));
        int attempt = 0;

        while (true)
        {
#pragma warning disable CA1031 // Do not catch general exception types
            try
            {
                await serviceLock.WaitAsync(threadIndex).ConfigureAwait(false);
                var response = await lookup.Invoke().ConfigureAwait(false);
                LogSuccess(writer, serviceUri, domain);
                return (true, response);
            }
            catch (RdapRequestException exception) when (exception.StatusCode == HttpStatusCode.NotFound)
            {
                DebugWriteLine($"serviceURI: {serviceUri}, domain:{domain} - NotFound");
                LogError(writer, serviceUri, domain, exception);
                return (false, null);
            }
            catch (RdapRequestException exception) when (exception.StatusCode == HttpStatusCode.TooManyRequests)
            {
                attempt++;
                DebugWriteLine($"serviceURI: {serviceUri}, domain:{domain} - TooManyRequests");
                if (attempt > 3)
                {
                    LogError(writer, serviceUri, domain, exception);
                    return (false, null);
                }
                await serviceLock.AddDelayAsync(exception.ResponseHeaders.RetryAfter?.Date, threadIndex).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                DebugWriteLine($"serviceURI: {serviceUri}, domain:{domain} - {exception.Message}");
                LogError(writer, serviceUri, domain, exception);
                return (false, null);
            }
#pragma warning restore CA1031 // Do not catch general exception types
        }
    }

    private static void DebugWriteLine(string message)
    {
        lock (_lock)
        {
            Console.WriteLine(message);
        }
    }

    private void LogError(StreamWriter writer, Uri? serviceUri, string domain, Exception exception)
    {
        if (writer == null)
        {
            return;
        }

        string? errorCode = null;
        string? filename = null;

        switch(exception)
        {
            case RdapRequestException requestException:
                errorCode = requestException.StatusCode.HasValue ? requestException.StatusCode.Value.ToString() : "UnknownStatusCode";
                break;
            case RdapBootstrapException bootstrapException:
                errorCode = "Bootstrap";
                break;
            case RdapSerializerException rdapSerializerException:
                {
                    errorCode = "Json";
                    filename = $"{Guid.NewGuid()}.json";
                    string jsonPath = Path.Combine(_resultPath, "json");
                    lock (_lock)
                    {
                        if (!Directory.Exists(jsonPath))
                        {
                            Directory.CreateDirectory(jsonPath);
                        }
                    }

                    using StreamWriter jsonWriter = new StreamWriter(Path.Combine(jsonPath, filename), false);
                    jsonWriter.Write(rdapSerializerException.ResponseBody);
                }
                break;
            default:
                errorCode = exception.GetType().Name;
                break;
        };

        lock (_lock)
        {
            WriteCsvLine(writer, errorCode, serviceUri, domain, exception.Message, filename);
        }
    }

    private static void LogSuccess(StreamWriter writer, Uri serviceUri, string domain)
    {
        if (writer == null)
        {
            return;
        }

        lock (_lock)
        {
            WriteCsvLine(writer, "OK", serviceUri, domain, error: null);
        }
    }

    private static void WriteCsvHeader(StreamWriter writer)
    {
        writer.WriteLine($"result,host,serviceUri,domain,error,filename");
    }

    private static void WriteCsvLine(StreamWriter writer, string result, Uri? serviceUri, string domain, string? error, string? filename = null)
    {
        writer.WriteLine($"{result},{GetCsvValue(serviceUri?.Host)},{GetCsvValue(serviceUri)},{GetCsvValue(domain)},\"{GetCsvValue(error?.Replace('"', '\''))}\",{GetCsvValue(filename)}");
    }

    private static string GetCsvValue(object? value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        return $"{value}";
    }

    private async Task LookupDomainAsync(
        RdapClient client,
        Uri uri,
        string domain,
        HashSet<string> hosts,
        StreamWriter writer,
        int threadIndex)
    {
        Console.WriteLine($"Looking up domain {uri}");
        var (success, result) = await TryExecuteLookupAsync(
            () => client.DomainLookupAsync(uri),
            uri,
            domain,
            writer,
            threadIndex).ConfigureAwait(false);
        if (success && result != null)
        {
            await ProcessDomainResponseAsync(client, result.Value, domain, hosts, writer, threadIndex).ConfigureAwait(false);
        }
    }

    private async Task ProcessDomainResponseAsync(
        RdapClient client,
        RdapDomainLookupResponse response,
        string domain,
        HashSet<string> hosts,
        StreamWriter writer,
        int threadIndex)
    {
        if (response.Links == null || response.Links.Count == 0)
        {
            return;
        }

        foreach (var link in response.Links)
        {
            var value = link.Value ?? link.Href;
            if (string.IsNullOrEmpty(value))
            {
                continue;
            }

            if (!value.EndsWith($"/domain/{domain}", StringComparison.OrdinalIgnoreCase))
            {
                continue;                
            }

            var serviceUri = new Uri(value);
            var host = serviceUri.Host.ToUpperInvariant();

            if (hosts.Contains(host))
            {
                continue;
            }

            hosts.Add(host);

            await LookupDomainAsync(client, serviceUri, domain, hosts, writer, threadIndex).ConfigureAwait(false);
        }
    }
}

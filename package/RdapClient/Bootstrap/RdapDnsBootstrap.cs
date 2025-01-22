using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Bootstrap;

public class RdapDnsBootstrap : RdapBootstrap
{
    private readonly IdnMapping idnMapping = new IdnMapping();

    public RdapDnsBootstrap(ILogger logger = null): base(logger)
    {

    }

    public override async Task<Uri> FindServiceUrlAsync(string value)
    {
        _ = value ?? throw new ArgumentNullException(nameof(value));

        var serviceDictionary = await GetServiceDictionaryAsync().ConfigureAwait(false);

        // do lookup from lowest level domain name up
        int dotIndex;
        value = idnMapping.GetAscii(value).ToUpperInvariant();
        do
        {
            if (serviceDictionary.TryGetValue(value, out var serviceUrlList) && serviceUrlList.Count > 0)
            {
                if (serviceUrlList.Count > 1)
                {
                    Uri url = SelectUrl(serviceUrlList);
                    serviceUrlList.Clear();
                    serviceUrlList.Add(url);
                }
                Logger?.LogInformation("Found service {Url} in registry for name {Name}", serviceUrlList[0], value);
                return serviceUrlList[0];
            }
            else
            {
                dotIndex = value.IndexOf('.', StringComparison.Ordinal);
                if (dotIndex > 0)
                {
                    Logger?.LogDebug("Registry does not contain service for name {Name} - trying parent domain", value);
                    value = value.Substring(dotIndex + 1);
                }
            }
        } while (dotIndex > 0);

        // nothing found
        Logger?.LogError("Unable to find any service url in domains service registry for domain name {Name}", value);
        throw new RdapBootstrapException($"Unable to find any service url in domains service registry for domain name '{value}'");
    }

    private async Task<Dictionary<string, List<Uri>>> GetServiceDictionaryAsync()
    {
        return await GetOrAddCacheItemAsync("dns", () => CreateServiceDictionaryAsync()).ConfigureAwait(false);
    }

    private async Task<Dictionary<string, List<Uri>>> CreateServiceDictionaryAsync()
    {
        var serviceRegistry = await GetRdapServiceRegistryAsync(new Uri("dns.json", UriKind.Relative)).ConfigureAwait(false);
        Logger?.LogDebug("Storing service registry data in memory.");

        var serviceDictionary = new Dictionary<string, List<Uri>>();
        foreach (var service in serviceRegistry.Services)
        {
            if (service.ServiceUrls == null || service.ServiceUrls.Count == 0)
            {
                continue;
            }

            foreach (string entry in service.Entries)
            {
                string key = entry.ToUpperInvariant();
                if (serviceDictionary.TryGetValue(key, out var serviceUrlList))
                {
                    serviceUrlList.AddRange(service.ServiceUrls);
                }
                else
                {
                    serviceDictionary.Add(key, service.ServiceUrls.ToList());
                }
            }
        }

        return serviceDictionary;
    }
}

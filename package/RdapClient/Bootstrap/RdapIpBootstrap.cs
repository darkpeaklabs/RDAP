using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetTools;

namespace DarkPeakLabs.Rdap.Bootstrap;

public class RdapIpBootstrap : RdapBootstrap
{
    public RdapIpBootstrap(ILogger logger = null) : base(logger)
    {

    }

    public override async Task<Uri> FindServiceUrlAsync(string value)
    {
        _ = value ?? throw new ArgumentNullException(nameof(value));

        if (!IPAddress.TryParse(value, out IPAddress address))
        {
            throw new RdapBootstrapException($"{value} is not a valid IP address");
        }

        var serviceLookupList = address.AddressFamily switch
        {
            AddressFamily.InterNetwork => await GetIpv4ServiceLookupListAsync().ConfigureAwait(false),
            AddressFamily.InterNetworkV6 => await GetIpv6ServiceLookupListAsync().ConfigureAwait(false),
            _ => throw new RdapBootstrapException($"{address.AddressFamily} is not supported IP address family")
        };

        foreach(var (IPAddressRanges, ServiceUrls) in serviceLookupList)
        {
            foreach(var addressRange in IPAddressRanges)
            {
                if (addressRange.Contains(address))
                {
                    return SelectUrl(ServiceUrls);
                }
            }
        }

        // nothing found
        Logger?.LogError("Unable to find any service url in IP service registry for address {Value}", value);
        throw new RdapBootstrapException($"Unable to find any service url in IP service registry for address '{value}'");
    }

    private async Task<List<(IReadOnlyCollection<IPAddressRange> IPAddressRanges, IReadOnlyCollection<Uri> ServiceUrls)>> GetIpv4ServiceLookupListAsync()
    {
        return await GetOrAddCacheItemAsync("ipv4", () => CreateIPv4ServiceLookupListAsync()).ConfigureAwait(false);
    }

    private async Task<List<(IReadOnlyCollection<IPAddressRange> IPAddressRanges, IReadOnlyCollection<Uri> ServiceUrls)>> GetIpv6ServiceLookupListAsync()
    {
        return await GetOrAddCacheItemAsync("ipv6", () => CreateIPv6ServiceLookupListAsync()).ConfigureAwait(false);
    }


    private async Task<List<(IReadOnlyCollection<IPAddressRange> IPAddressRanges, IReadOnlyCollection<Uri> ServiceUrls)>> CreateIPv4ServiceLookupListAsync()
    {
        Logger?.LogDebug("Creating IPv4 service lookup list");

        var serviceRegistry = await GetRdapServiceRegistryAsync(new Uri("ipv4.json", UriKind.Relative)).ConfigureAwait(false);

        return CreateIPAddressServiceLookupList(serviceRegistry);
    }

    private async Task<List<(IReadOnlyCollection<IPAddressRange> IPAddressRanges, IReadOnlyCollection<Uri> ServiceUrls)>> CreateIPv6ServiceLookupListAsync()
    {
        Logger?.LogDebug("Creating IPv6 service lookup list");

        var serviceRegistry = await GetRdapServiceRegistryAsync(new Uri("ipv6.json", UriKind.Relative)).ConfigureAwait(false);

        return CreateIPAddressServiceLookupList(serviceRegistry);
    }


    private static List<(IReadOnlyCollection<IPAddressRange> IPAddressRanges, IReadOnlyCollection<Uri> ServiceUrls)> CreateIPAddressServiceLookupList(RdapServiceRegistry serviceRegistry)
    {
        var lookupList = new List<(IReadOnlyCollection<IPAddressRange> IPAddressRanges, IReadOnlyCollection<Uri> ServiceUrls)>();

        foreach (var service in serviceRegistry.Services)
        {
            if (service.ServiceUrls == null || service.ServiceUrls.Count == 0)
            {
                continue;
            }

            List<IPAddressRange> ipAddressRanges = [];

            foreach (string entry in service.Entries)
            {
                if (!IPAddressRange.TryParse(entry, out var ipRange))
                {
                    continue;
                }

                ipAddressRanges.Add(ipRange);
            }

            if (ipAddressRanges.Count > 0)
            {
                lookupList.Add((ipAddressRanges, service.ServiceUrls));
            }
        }

        return lookupList;
    }
}

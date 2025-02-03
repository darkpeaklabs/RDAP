using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// IANA RDAP service registry client
/// </summary>
public class IANARdapServiceRegistryClient : RdapServiceRegistryClient
{
    /// <summary>
    /// Base URL for IANA RDAP service registry JSON files
    /// </summary>
    private const string IANABootstrapUrl = "https://data.iana.org/rdap/";

    /// <summary>
    /// Creates a new instance of <see cref="IANARdapServiceRegistryClient"/>
    /// </summary>
    public IANARdapServiceRegistryClient() : base()
    {
        BaseAddress = new Uri(IANABootstrapUrl);
    }

    /// <summary>
    /// Creates a new instance of <see cref="IANARdapServiceRegistryClient"/>
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    public IANARdapServiceRegistryClient(HttpMessageHandler handler) : base(handler)
    {
        BaseAddress = new Uri(IANABootstrapUrl);
    }

    /// <summary>
    /// Creates a new instance of <see cref="IANARdapServiceRegistryClient"/>
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    /// <param name="disposeHandler">Dispose handler</param>
    public IANARdapServiceRegistryClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
    {
        BaseAddress = new Uri(IANABootstrapUrl);
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for Domain Name Space
    /// </summary>
    /// <returns></returns>
    public async Task<RdapServiceRegistry> GetDnsRegistryAsync()
    {
        return await GetRegistryAsync(new Uri("dns.json", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for Domain Name Space
    /// </summary>
    /// <returns></returns>
    public RdapServiceRegistry GetDnsRegistry()
    {
        return GetDnsRegistryAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for AS Number Space
    /// </summary>
    /// <returns></returns>
    public async Task<RdapServiceRegistry> GetAsnRegistryAsync()
    {
        return await GetRegistryAsync(new Uri("asn.json", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for AS Number Space
    /// </summary>
    /// <returns></returns>
    public RdapServiceRegistry GetAsnRegistry()
    {
        return GetAsnRegistryAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for IPv4 Address Space
    /// </summary>
    /// <returns></returns>
    public async Task<RdapServiceRegistry> GetIPv4RegistryAsync()
    {
        return await GetRegistryAsync(new Uri("ipv4.json", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for IPv4 Address Space
    /// </summary>
    /// <returns></returns>
    public RdapServiceRegistry GetIPv4Registry()
    {
        return GetIPv4RegistryAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for IPv6 Address Space
    /// </summary>
    /// <returns></returns>
    public async Task<RdapServiceRegistry> GetIPv6RegistryAsync()
    {
        return await GetRegistryAsync(new Uri("ipv6.json", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for IPv6 Address Space
    /// </summary>
    /// <returns></returns>
    public RdapServiceRegistry GetIPv6Registry()
    {
        return GetIPv6RegistryAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for Provider Object Tags
    /// </summary>
    /// <returns></returns>
    public async Task<RdapObjectTagsServiceRegistry> GetObjectTagsRegistryAsync()
    {
        return await GetObjectTagsRegistryAsync(new Uri("object-tags.json", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Bootstrap Service Registry for Provider Object Tags
    /// </summary>
    /// <returns></returns>
    public RdapObjectTagsServiceRegistry GetObjectTagsRegistry()
    {
        return GetObjectTagsRegistryAsync().GetAwaiter().GetResult();
    }
}

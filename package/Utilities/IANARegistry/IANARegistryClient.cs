using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Utilities;

/// <summary>
/// IANA registry Http client
/// </summary>
public class IANARegistryClient : CsvHttpClient
{
    /// <summary>
    /// Creates a new instance of <see cref="IANARegistryClient"/> class
    /// </summary>
    public IANARegistryClient() : base()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="IANARegistryClient"/> class
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    public IANARegistryClient(HttpMessageHandler handler) : base(handler)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="IANARegistryClient"/> class
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    /// <param name="disposeHandler">Dispose handler</param>
    public IANARegistryClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
    {
    }

    /// <summary>
    /// Get IANA registrar IDs
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<IANARegistrar>> GetRegistrarIdsAsync()
    {
        return await GetRecordsAsync<IANARegistrar>(new Uri("https://www.iana.org/assignments/registrar-ids/registrar-ids-1.csv")).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA registrar IDs
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IANARegistrar> GetRegistrarIds()
    {
        return GetRegistrarIdsAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Get IANA RDAP Json values registry
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<RdapJsonValue>> GetRdapJsonValuesAsync()
    {
        return await GetRecordsAsync<RdapJsonValue>(new Uri("https://www.iana.org/assignments/rdap-json-values/rdap-json-values-1.csv")).ConfigureAwait(false);
    }

    public async Task<IEnumerable<DnsSecAlgorithmNumber>> GetDnsSecAlgorithmNumbersAsync()
    {
        return await GetRecordsAsync<DnsSecAlgorithmNumber>(new Uri("https://www.iana.org/assignments/dns-sec-alg-numbers/dns-sec-alg-numbers-1.csv")).ConfigureAwait(false);
    }

    public async Task<IEnumerable<DnsSecDigestTypeValue>> GetDnsSecDigestTypesAsync()
    {
        return await GetRecordsAsync<DnsSecDigestTypeValue>(new Uri("https://www.iana.org/assignments/ds-rr-types/ds-rr-types-1.csv")).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Link Relations registry
    /// </summary>
    /// <returns></returns>
    public IEnumerable<RdapJsonValue> GetRdapJsonValues()
    {
        return GetRdapJsonValuesAsync().GetAwaiter().GetResult();
    }

    /// <summary>
    /// Get IANA Link Relations registry
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<LinkRelation>> GetLinkRelationsAsync()
    {
        return await GetRecordsAsync<LinkRelation>(new Uri("https://www.iana.org/assignments/link-relations/link-relations-1.csv")).ConfigureAwait(false);
    }

    /// <summary>
    /// Get IANA Link Relations registry
    /// </summary>
    /// <returns></returns>
    public IEnumerable<LinkRelation> GetLinkRelations()
    {
        return GetLinkRelationsAsync().GetAwaiter().GetResult();
    }
}

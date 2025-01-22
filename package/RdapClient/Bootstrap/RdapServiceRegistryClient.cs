using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// RDAP service registry client
/// </summary>
public class RdapServiceRegistryClient : HttpClient
{
    /// <summary>
    /// Creates a new instance of <see cref="RdapServiceRegistryClient"/> class
    /// </summary>
    public RdapServiceRegistryClient() : base()
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="RdapServiceRegistryClient"/> class
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    public RdapServiceRegistryClient(HttpMessageHandler handler) : base(handler)
    {
    }

    /// <summary>
    /// Creates a new instance of <see cref="RdapServiceRegistryClient"/> class
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    /// <param name="disposeHandler">Dispose handler</param>
    public RdapServiceRegistryClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
    {
    }

    /// <summary>
    /// Get Bootstrap Service Registry 
    /// </summary>
    /// <param name="requestUri">Request Url</param>
    /// <returns></returns>
    public async Task<RdapServiceRegistry> GetRegistryAsync(Uri requestUri)
    {
        return await GetRegistryAsync<RdapServiceRegistry>(
            requestUri,
            new List<JsonConverter>() { new RdapServiceRegistryItemConverter() })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Get Bootstrap Service Registry for Provider Object Tags
    /// </summary>
    /// <param name="requestUri">Request Url</param>
    /// <returns></returns>
    public async Task<RdapObjectTagsServiceRegistry> GetObjectTagsRegistryAsync(Uri requestUri)
    {
        return await GetRegistryAsync<RdapObjectTagsServiceRegistry>(
            requestUri,
            new List<JsonConverter>() { new RdapObjectTagsServiceRegistryItemConverter() })
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Get Bootstrap Service Registry
    /// </summary>
    /// <typeparam name="T">Bootstrap Service Registry data type</typeparam>
    /// <param name="requestUri">Request Url</param>
    /// <param name="converters">Optional JSON converters</param>
    /// <returns></returns>
    private async Task<T> GetRegistryAsync<T>(Uri requestUri, List<JsonConverter> converters = null)
    {
        string json = await GetStringAsync(requestUri).ConfigureAwait(false);

        JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
        if (converters != null)
        {
            converters.ForEach(x => serializerOptions.Converters.Add(x));
        }

        return JsonSerializer.Deserialize<T>(json, serializerOptions);
    }

}

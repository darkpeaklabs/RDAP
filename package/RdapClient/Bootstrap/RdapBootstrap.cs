using LazyCache;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Bootstrap;

public abstract class RdapBootstrap
{
    private readonly CachingService cache = new();

    protected ILogger Logger { get; private set; }

    protected RdapBootstrap(ILogger logger = null)
    {
        Logger = logger;
    }

    protected static async Task<RdapServiceRegistry> GetRdapServiceRegistryAsync(Uri relativeUri)
    {
        _ = relativeUri ?? throw new ArgumentNullException(nameof(relativeUri));

        using var client = new IANARdapServiceRegistryClient();
        return await client.GetRegistryAsync(relativeUri).ConfigureAwait(false);
    }

    /// <summary>
    /// Picks a url from a list with preference given to HTTPS urls
    /// </summary>
    /// <param name="urls">list of urls</param>
    /// <returns></returns>
    protected static Uri SelectUrl(IReadOnlyCollection<Uri> urls)
    {
        _ = urls ?? throw new ArgumentNullException(paramName: nameof(urls));

        foreach (Uri url in urls)
        {
            if (url.Scheme == Uri.UriSchemeHttps)
            {
                return url;
            }
        }
        return urls.First();
    }

    protected async Task<T> GetOrAddCacheItemAsync<T>(string key, Func<Task<T>> addItemFactory)
    {
        return await cache.GetOrAddAsync<T>($"__rdap_bootstrap_{key}__", addItemFactory).ConfigureAwait(false);
    }

    public abstract Task<Uri> FindServiceUrlAsync(string value);
}

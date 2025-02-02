using DarkPeakLabs.Rdap.Conformance;
using DarkPeakLabs.Rdap.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap;

/// <summary>
/// Class representing RDAP web service endpoint
/// </summary>
public sealed class RdapClient : IDisposable
{
    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger _logger;

    private readonly HttpClient httpClient;
    private readonly RdapHttpClientHandler httpClientHandler;
    private bool disposedValue;
    private X509Certificate2 clientCertificate;
    private readonly IdnMapping idnMapping = new();

    public TimeSpan Timeout { get => httpClient.Timeout; set => httpClient.Timeout = value; }

    public HttpRequestHeaders HttpRequestHeaders => httpClient.DefaultRequestHeaders;

    /// <summary>
    /// Client class constructor
    /// Initializes HttpClient
    /// </summary>
    /// <param name="clientHandler">Instance of client handler class to use</param>
    public RdapClient(ILogger logger = null)
    {
        _logger = logger;

        httpClientHandler = new RdapHttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) =>
            {
                foreach (var chainStatus in certChain.ChainStatus.Where(x => x.Status != X509ChainStatusFlags.NoError))
                {
                    //Conformance?.AddImplementationViolation(RdapConformanceViolationSeverity.Error, chainStatus.StatusInformation);
                }
                return true;
            }
        };

        httpClient = new HttpClient(httpClientHandler, false);

        // setting HttpClient.Timeout ensures TaskCanceledException is thrown when timeout is exceeded
        httpClient.Timeout = httpClient.Timeout;

        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/rdap+json"));

        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        var rdapClientType = GetType();
        httpClient.DefaultRequestHeaders.UserAgent.Add(
            new ProductInfoHeaderValue(
                rdapClientType.FullName,
                rdapClientType.Assembly.GetName().Version.ToString()));
    }

    public void AddBasicAuthentication(string username, SecureString password)
    {
        _ = username ?? throw new ArgumentNullException(paramName: nameof(username));
        _ = password ?? throw new ArgumentNullException(paramName: nameof(password));

        var networkCredential = new NetworkCredential(username, password);
        var bytes = Encoding.ASCII.GetBytes($"{networkCredential.UserName}:{networkCredential.Password}");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));
    }

    public void AddClientCertificate(StoreName storeName, StoreLocation storeLocation, string thumbprint)
    {
        _ = thumbprint ?? throw new ArgumentNullException(paramName: nameof(thumbprint));


        using (var store = new X509Store(storeName, storeLocation))
        {
            store.Open(OpenFlags.ReadOnly);
            var collection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
            if (collection.Count == 0)
            {
                throw new ArgumentException($"Certificate {thumbprint} not found.");
            }
            clientCertificate = new X509Certificate2(collection[0]);
        }

        httpClientHandler.ClientCertificates.Add(clientCertificate);
    }

    /// <summary>
    /// request helpful information (command syntax, terms of service, privacy policy,
    /// rate-limiting policy, supported authentication methods, supported extensions,
    /// technical support contact, etc.) from an RDAP server
    /// <para>
    /// <see cref="https://tools.ietf.org/html/rfc7482#section-3.1.6">RFC 7482</see>
    /// </para>        /// </summary>
    /// <returns></returns>
    public async Task<RdapLookupResult<RdapHelpResponse>> HelpAsync(Uri serviceUri)
    {
        _logger?.LogInformation("Sending request for help information");
        return await GetDataAsync<RdapHelpResponse>(serviceUri, new Uri($"help", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Queries for domain information are of the form /domain/XXXX/...,
    /// where XXXX is a fully qualified(relative to the root) domain name (as specified in [RFC0952] and [RFC1123]) in either the in-addr.arpa
    /// or ip6.arpa zones(for RIRs) or a fully qualified domain name in a zone administered by the server operator (for DNRs)
    /// <para>
    /// <see cref="https://tools.ietf.org/html/rfc7482#section-3.1.3">RFC 7482</see>
    /// </para>
    /// </summary>
    /// <param name="domain">domain name</param>
    /// <returns>Domain lookup response</returns>
    public async Task<RdapLookupResult<RdapDomainLookupResponse>> DomainLookupAsync(Uri serviceUri, string domain)
    {
        _logger?.LogInformation("Sending lookup query for domain {Domain}", domain);
        domain = idnMapping.GetAscii(domain);
        return await GetDataAsync<RdapDomainLookupResponse>(serviceUri, new Uri($"domain/{domain}", UriKind.Relative)).ConfigureAwait(false);
    }

    public async Task<RdapLookupResult<RdapDomainLookupResponse>> DomainLookupAsync(Uri requestUri)
    {
        return await GetDataAsync<RdapDomainLookupResponse>(requestUri).ConfigureAwait(false);
    }

    /// <summary>
    /// Searches for domain information by name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public async Task<RdapLookupResult<RdapDomainSearchResponse>> DomainSearchByNameAsync(Uri serviceUri, string name)
    {
        _logger?.LogInformation("Sending search query for domains by name {Name}", name);
        return await GetDataAsync<RdapDomainSearchResponse>(serviceUri, new Uri($"domains?name={name}", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Searches for domain information by nameserver IP address
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<RdapLookupResult<RdapDomainSearchResponse>> DomainSearchByNameServerIpAsync(Uri serviceUri, string ip)
    {
        _logger?.LogInformation("Sending search query for domains by name server ip {Ip}", ip);
        return await GetDataAsync<RdapDomainSearchResponse>(serviceUri, new Uri($"domains?ip={ip}", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// <para>
    /// Searches for domain information by nameserver name using this form: /domains?nsLdhName=YYYY
    /// </para>
    /// <para>
    /// YYYY is a search pattern representing a host name in "letters,
    /// digits, hyphen" format [RFC5890] in a zone administered by the server
    /// operator of a DNR.The following URL would be used to search for
    /// domains delegated to nameservers matching the "ns1.example*.com"
    /// pattern.
    /// </para>
    /// <para>
    /// Example: https://example.com/rdap/domains?nsLdhName=ns1.example*.com
    /// </para>
    /// </summary>
    /// <param name="nsLdhName">nameserver name</param>
    /// <returns>Search response</returns>
    public async Task<RdapLookupResult<RdapDomainSearchResponse>> DomainSearchByNameServerNameAsync(Uri serviceUri, string nsLdhName)
    {
        _logger?.LogInformation("Sending search query for domains by name server name {Name}", nsLdhName);
        return await GetDataAsync<RdapDomainSearchResponse>(serviceUri, new Uri($"domains?nsLdhName={nsLdhName}", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Queries for information about IP networks are of the form /ip/XXX/... or /ip/XXX/YY/...
    /// where the path segment following 'ip' is either an IPv4 dotted decimal or IPv6[RFC5952] address(i.e., XXX) or an IPv4
    /// or IPv6 Classless Inter-domain Routing(CIDR) [RFC4632] notation address block(i.e., XXX/YY).
    /// <para>
    /// <see cref="https://tools.ietf.org/html/rfc7482#section-3.1.1">RFC 7482</see>
    /// </para>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<RdapLookupResult<RdapIPNetworkLookupResponse>> IpLookupAsync(Uri serviceUri, string ip)
    {
        _logger?.LogInformation("Sending lookup query for IP = {Ip}", ip);
        return await GetDataAsync<RdapIPNetworkLookupResponse>(serviceUri, new Uri($"ip/{ip}", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Queries for information regarding Autonomous System number
    /// registrations are of the form /autnum/XXX/... where XXX is an asplain
    /// Autonomous System number[RFC5396].
    /// </summary>
    /// <para>
    /// <see cref="https://tools.ietf.org/html/rfc7482#section-3.1.2">RFC 7482</see>
    /// </para>
    /// <param name="asn"></param>
    /// <returns></returns>
    public async Task<RdapLookupResult<RdapAutnumLookupResponse>> AutnumLookupAsync(Uri serviceUri, int asn)
    {
        _logger?.LogInformation("Sending lookup query for autnum where ASN = {Asn}", asn);
        return await GetDataAsync<RdapAutnumLookupResponse>(serviceUri, new Uri($"autnum/{asn}", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Queries for information regarding name servers
    /// <para>
    /// <see cref="https://tools.ietf.org/html/rfc7482#section-3.1.4">RFC 7482</see>
    /// </para>
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    public async Task<RdapLookupResult<RdapNameServerLookupResponse>> NameServerLookupAsync(Uri serviceUri, string server)
    {
        _logger?.LogInformation("Sending lookup query for name server {Server}", server);
        return await GetDataAsync<RdapNameServerLookupResponse>(serviceUri, new Uri($"nameserver/{server}", UriKind.Relative)).ConfigureAwait(false);
    }

    /// <summary>
    /// Queries for information regarding entity by handle. The <handle> parameter represents an entity (such as a contact,
    /// registrant, or registrar) identifier whose syntax is specific to the
    /// registration provider.
    /// <para>
    /// <see cref="https://tools.ietf.org/html/rfc7482#section-3.1.5">RFC 7482</see>
    /// </para>
    /// </summary>
    /// <param name="handle">Entity handle</param>
    /// <returns>Entity response object</returns>
    public async Task<RdapLookupResult<RdapEntityLookupResponse>> EntityLookupAsync(Uri serviceUri, string handle)
    {
        _logger?.LogInformation("Sending lookup query for entity with handle = {Handle}", handle);
        return await GetDataAsync<RdapEntityLookupResponse>(serviceUri, new Uri($"entity/{handle}", UriKind.Relative)).ConfigureAwait(false);
    }

    private async Task<RdapLookupResult<T>> GetDataAsync<T>(Uri serviceUri, Uri relativeUri) where T : class
    {
        return await GetDataAsync<T>(GetRequestUri(serviceUri, relativeUri)).ConfigureAwait(false);
    }

    private async Task<RdapLookupResult<T>> GetDataAsync<T>(Uri requestUri) where T : class
    {
        RdapLookupResult<T> result = new()
        {
            Conformance = new()
        };

        _logger?.LogInformation("Sending request to RDAP web service. Query URL = {Url}", requestUri);

        try
        {
            using var response = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            result.RequestSent = httpClientHandler.RequestSent;
            result.ResponseReceived = httpClientHandler.ResponseReceived;

            if (response.IsSuccessStatusCode)
            {
                result.DataReadStarted = DateTime.UtcNow;
                result.HttpResponseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                result.HttpVersion = response.Version;
                result.HttpResponseHeaders = response.Headers;
                result.HttpRequestHeaders = response.RequestMessage.Headers;
                result.HttpRequestMethod = response.RequestMessage.Method;
                result.HttpRequestUri = response.RequestMessage.RequestUri;
                result.DataReadFinished = DateTime.UtcNow;

                if (response.Content.Headers != null)
                {
                    result.HttpContentHeaders = response.Content.Headers;
                    var contentType = response.Content.Headers.ContentType?.MediaType;

                    if (contentType == null)
                    {
                        result.Conformance.AddImplementationViolation(
                            RdapConformanceViolationSeverity.Warning,
                            $"Server did not include expected application/rdap+json Content-Type header value");
                    }
                    else if (contentType != "application/rdap+json")
                    {
                        result.Conformance.AddImplementationViolation(
                            RdapConformanceViolationSeverity.Warning,
                            $"{response.Content.Headers.ContentType.MediaType} differs from expected application/rdap+json Content-Type header value");
                    }
                }
            }
            else
            {
                _logger?.LogError("Server responded {StatusCode} - url:{RdapQuery}, content length: {ContentLength}", response.StatusCode, requestUri, response.Content.Headers.ContentLength);
                throw new RdapRequestException(response);
            }
        }
        catch (RdapRequestException)
        {
            // rethrow
            throw;
        }
        catch (TaskCanceledException exception)
        {
            result.RequestSent = httpClientHandler.RequestSent;
            result.RequestFailed = DateTime.UtcNow;

            throw new RdapClientTimeoutException($"RDAP server did not respond in allocated time {httpClient.Timeout}", exception);
        }
        catch (HttpRequestException exception)
        {
            result.RequestSent = httpClientHandler.RequestSent;
            result.RequestFailed = DateTime.UtcNow;

            throw new RdapHttpException("Error sending request to RDAP Server", exception);
        }
        catch (Exception exception)
        {
            result.RequestSent = httpClientHandler.RequestSent;
            result.RequestFailed = DateTime.UtcNow;
            throw new RdapHttpException(exception.Message, exception);
        }

        try
        {
            result.Value = RdapSerializer.Deserialize<T>(result.HttpResponseBody, result.Conformance);
            return result;
        }
        //catch (Exception exception)
        //{
        //    throw new RdapSerializerException(
        //        exception.Message,
        //        result.HttpResponseBody,
        //        exception);
        //}
        finally
        {

        }
    }

    private static Uri GetRequestUri(Uri serviceUri, Uri relativeUri)
    {
        if (serviceUri.AbsoluteUri.EndsWith('/'))
        {
            return new Uri(serviceUri, relativeUri);
        }

        return new Uri(new Uri($"{serviceUri.AbsoluteUri}/"), relativeUri);
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                httpClientHandler.Dispose();
                httpClient.Dispose();
                clientCertificate?.Dispose();
            }
            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap;

internal class RdapHttpClientHandler : HttpClientHandler
{
    internal DateTime? RequestSent { get; private set; }

    internal DateTime? ResponseReceived { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        RequestSent = DateTime.UtcNow;
        var response = base.SendAsync(request, cancellationToken);
        ResponseReceived = DateTime.UtcNow;
        return response;
    }
}

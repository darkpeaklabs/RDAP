using System;
using System.Net.Http;
using System.Net.Http.Headers;
using DarkPeakLabs.Rdap.Conformance;

namespace DarkPeakLabs.Rdap
{
    public class RdapLookupResult<T> where T : class
    {
        /// <summary>
        /// Raw string Json response from last successful request
        /// </summary>
        public string HttpResponseBody { get; internal set; }
        public HttpResponseHeaders HttpResponseHeaders { get; internal set; }
        public HttpRequestHeaders HttpRequestHeaders { get; internal set; }
        public Version HttpVersion { get; internal set; }

        /// <summary>
        ///
        /// </summary>
        public RdapConformance Conformance { get; internal set; }

        public T Value { get; internal set; }

        public DateTimeOffset? RequestSent { get; internal set; }
        public DateTimeOffset? ResponseReceived { get; internal set; }
        public DateTimeOffset? RequestFailed { get; internal set; }
        public DateTimeOffset? DataReadStarted { get; internal set; }
        public DateTimeOffset? DataReadFinished { get; internal set; }
        public HttpMethod HttpRequestMethod { get; internal set; }
        public Uri HttpRequestUri { get; internal set; }
        public HttpContentHeaders HttpContentHeaders { get; internal set; }
    }
}

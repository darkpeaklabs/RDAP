using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// RDAP request exception
    /// </summary>
    public class RdapRequestException : RdapException
    {
        public HttpStatusCode? StatusCode { get; private set; }
        public string ReasonPhrase { get; private set; }

        public HttpResponseHeaders ResponseHeaders { get; private set; }
        public Version Version { get; }
        public HttpRequestHeaders RequestHeaders { get; }
        public HttpMethod RequestMethod { get; }
        public Uri RequestUri { get; }

        public RdapRequestException()
        {
        }

        public RdapRequestException(string message) : base(message)
        {
        }

        public RdapRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        internal RdapRequestException(HttpResponseMessage response) : base($"Rdap Service responded {response?.StatusCode}")
        {
            _ = response ?? throw new ArgumentNullException(paramName: nameof(response));

            StatusCode = response.StatusCode;
            ReasonPhrase = response.ReasonPhrase;
            ResponseHeaders = response.Headers;
            Version = response.Version;
            RequestHeaders = response.RequestMessage.Headers;
            RequestMethod = response.RequestMessage.Method;
            RequestUri = response.RequestMessage.RequestUri;
        }

        public override string ToString()
        {
            return $"Server responded {StatusCode}";
        }
    }
}

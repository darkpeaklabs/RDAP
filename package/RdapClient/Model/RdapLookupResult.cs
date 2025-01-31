using System;
using DarkPeakLabs.Rdap.Conformance;

namespace DarkPeakLabs.Rdap
{
    public class RdapLookupResult<T> where T : class
    {
        /// <summary>
        /// Raw string Json response from last successful request
        /// </summary>
        public string RawJson { get; internal set; }

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
    }
}

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

        public DateTime? RequestSent { get; internal set; }
        public DateTime? ResponseReceived { get; internal set; }
        public DateTime? RequestFailed { get; internal set; }
        public DateTime? DataReadStarted { get; internal set; }
        public DateTime? DataReadFinished { get; internal set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Base class for DNSSEC record
    /// </summary>
    public class RdapDnsSecRecordBase
    {
        /// <summary>
        /// links to other resources on the Internet
        /// </summary>
        [JsonPropertyName("links")]
        public IReadOnlyCollection<RdapLink> Links { get; set; }

        /// <summary>
        /// events that have occurred on an instance of an object class
        /// </summary>
        [JsonPropertyName("events")]
        public IReadOnlyCollection<RdapEvent> Events { get; set; }
    }
}
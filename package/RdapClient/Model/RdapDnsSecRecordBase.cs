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
        public IReadOnlyList<RdapLink> Links { get; set; }

        /// <summary>
        /// events that have occurred on an instance of an object class
        /// </summary>
        [JsonPropertyName("events")]
        public IReadOnlyList<RdapEvent> Events { get; set; }
    }
}

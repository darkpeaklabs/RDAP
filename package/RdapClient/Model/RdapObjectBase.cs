using DarkPeakLabs.Rdap.Values.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Base class for response object classes
    /// </summary>
    public class RdapObjectBase
    {
        /// <summary>
        /// the string "entity"
        /// </summary>
        [JsonPropertyName("objectClassName")]
        public RdapObjectClass ObjectClass { get; set; }

        /// <summary>
        /// a string representing a registry unique identifier of the object
        /// </summary>
        [JsonPropertyName("handle")]
        public RdapObjectHandle Handle { get; set; }

        /// <summary>
        /// an array of entity objects
        /// </summary>
        [JsonPropertyName("entities")]
        public IReadOnlyCollection<RdapEntity> Entities { get; set; }

        /// <summary>
        /// denotes information about the object class that contains it(see Section 5 regarding object classes)
        /// </summary>
        [JsonPropertyName("remarks")]
        public IReadOnlyCollection<RdapRemark> Remarks { get; set; }

        /// <summary>
        /// links to other resources on the Internet
        /// </summary>
        [JsonPropertyName("links")]
        public IReadOnlyCollection<RdapLink> Links { get; set; }

        /// <summary>
        /// events that have occurred on an instance of an object class (see Section 5 regarding object classes)
        /// </summary>
        [JsonPropertyName("events")]
        public IReadOnlyCollection<RdapEvent> Events { get; set; }

        /// <summary>
        /// an array of strings indicating the state of a registered object
        /// </summary>
        [JsonPropertyName("status")]
        public IReadOnlyCollection<RdapStatus> Status { get; set; }

        /// <summary>
        /// a simple string containing the fully qualified host name or IP address of the WHOIS [RFC3912] server where the containing object instance may be found
        /// </summary>
        [JsonPropertyName("port43")]
        public string Port43 { get; set; }
    }
}

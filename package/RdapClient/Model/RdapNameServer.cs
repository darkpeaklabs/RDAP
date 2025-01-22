using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// The nameserver object class represents information regarding DNS nameservers used in both forward and reverse DNS
    /// </summary>
    public class RdapNameServer : RdapNamedObjectBase
    {
        /// <summary>
        /// an object containing name server IP addresses
        /// </summary>
        [JsonPropertyName("ipAddresses")]
        public RdapIPAddresses IPAddresses { get; set; }
    }
}
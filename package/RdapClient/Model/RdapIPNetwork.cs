using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// The IP network object class models IP network registrations found in RIRs
    /// </summary>
    public class RdapIPNetwork : RdapObjectBase
    {
        // TODO: convert to network IP classes

        /// <summary>
        /// the starting IP address of the network, either IPv4 or IPv6
        /// </summary>
        [JsonPropertyName("startAddress")]
        public string StartAddress { get; set; }

        /// <summary>
        /// the ending IP address of the network, either IPv4 or IPv6
        /// </summary>
        [JsonPropertyName("endAddress")]
        public string EndAddress { get; set; }

        /// <summary>
        /// a string signifying the IP protocol version of the network: "v4" signifies an IPv4 network, and "v6" signifies an IPv6 network
        /// </summary>
        [JsonPropertyName("ipVersion")]
        public string IPVersion { get; set; }

        /// <summary>
        /// an identifier assigned to the network registration by the registration holder
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// a string containing an RIR-specific classification of the network
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// a string containing the two-character country code of the network
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }

        /// <summary>
        /// a string containing an RIR-unique identifier of the parent network of this network registration
        /// </summary>
        [JsonPropertyName("parentHandle")]
        public string ParentHandle { get; set; }
    }
}
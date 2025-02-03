using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// a class containing list of name server IPv4 and v6 addresses
    /// </summary>
    public class RdapIPAddresses
    {
        /// <summary>
        /// an array of strings containing IPv6 addresses of the nameserver
        /// </summary>
        [JsonPropertyName("v6")]
        public IReadOnlyList<string> IPv6Addresses { get; set; }

        /// <summary>
        /// an array of strings containing IPv4 addresses of the nameserver
        /// </summary>
        [JsonPropertyName("v4")]
        public IReadOnlyList<string> IPv4Addresses { get; set; }
    }
}
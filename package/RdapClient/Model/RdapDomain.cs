using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// The domain object class represents a DNS name and point of delegation
    /// For RIRs, these delegation points are in the reverse DNS tree, whereas for DNRs, these delegation points are in the forward DNS tree.
    /// <see cref="https://tools.ietf.org/html/rfc7483#section-5.3">RFC7483</see>
    /// </summary>
    public class RdapDomain : RdapNamedObjectBase
    {
        /// <summary>
        /// an array of variant objects
        /// </summary>
        [JsonPropertyName("variants")]
        public IReadOnlyList<RdapDomainVariant> Variants { get; set; }

        /// <summary>
        /// an array of nameserver objects
        /// </summary>
        [JsonPropertyName("nameservers")]
        public IReadOnlyList<RdapNameServer> NameServers { get; set; }

        /// <summary>
        /// an object representing secure DNS record
        /// </summary>
        [JsonPropertyName("secureDNS")]
        public RdapSecureDns SecureDNS { get; set; }

        /// <summary>
        /// List of public identifiers to an object class
        /// </summary>
        [JsonPropertyName("publicIds")]
        public IReadOnlyList<RdapPublicId> PublicIds { get; set; }

        /// <summary>
        /// represents the IP network for which a reverse DNS domain is referenced
        /// </summary>
        [JsonPropertyName("network")]
        public RdapIPNetwork Network { get; set; }
    }
}

using DarkPeakLabs.Rdap.Values;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// DNSKEY record class
    /// </summary>
    public class RdapDnsKeyRecord : RdapDnsSecRecordBase
    {
        // TODO enums

        /// <summary>
        /// an integer representing the flags field value in the DNSKEY record RFC4034 in presentation format
        /// </summary>
        [JsonPropertyName("flags")]
        public DnsSecKeyFlags? Flags { get; set; }

        /// <summary>
        /// an integer representation of the protocol field value of the DNSKEY record RFC4034 in presentation format
        /// </summary>
        [JsonPropertyName("protocol")]
        public int? Protocol { get; set; }

        /// <summary>
        /// a string representation of the public key in the DNSKEY record [RFC4034] in presentation format
        /// </summary>
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }

        /// <summary>
        /// an integer as specified by the algorithm field of a DNSKEY record as described by RFC 4034 in presentation format
        /// </summary>
        [JsonPropertyName("algorithm")]
        public DnsSecAlgorithmType? Algorithm { get; set; }
    }
}

using DarkPeakLabs.Rdap.Values;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// class for secure DNS DS data object
    /// </summary>
    public class RdapDnsDsRecord : RdapDnsSecRecordBase
    {
        // TODO enums

        /// <summary>
        /// an integer as specified by the key tag field of a DNS DS record as specified by RFC4034 in presentation format
        /// </summary>
        [JsonPropertyName("keyTag")]
        public int? KeyTag { get; set; }

        /// <summary>
        /// an integer as specified by the algorithm field of a DNS DS record as described by RFC 4034 in presentation format
        /// </summary>
        [JsonPropertyName("algorithm")]
        public DnsSecAlgorithmType? Algorithm { get; set; }

        /// <summary>
        /// a string as specified by the digest field of a DNS DS record as specified by RFC 4034 in presentation format
        /// </summary>
        [JsonPropertyName("digest")]
        public string Digest { get; set; }

        /// <summary>
        /// an integer as specified by the digest type field of a DNS DS record as specified by RFC 4034 in presentation format
        /// </summary>
        [JsonPropertyName("digestType")]
        public DnsSecDigestType? DigestType { get; set; }
    }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Class fore secure DNS object
    /// </summary>
    public class RdapSecureDns
    {
        /// <summary>
        /// true if the zone has been signed, false otherwise
        /// </summary>
        [JsonPropertyName("zoneSigned")]
        public bool? ZoneSigned { get; set; }

        /// <summary>
        /// boolean true if there are DS records in the parent, false otherwise
        /// </summary>
        [JsonPropertyName("delegationSigned")]
        public bool? DelegationSigned { get; set; }

        /// <summary>
        /// an integer representing the signature lifetime in seconds to be used when creating the RRSIG DS record in the parent zone
        /// </summary>
        [JsonPropertyName("maxSigLife")]
        public int? MaxSigLife { get; set; }

        /// <summary>
        /// an array of objects representing DNS DS records
        /// </summary>
        [JsonPropertyName("dsData")]
        public IReadOnlyList<RdapDnsDsRecord> DnsDsRecords { get; set; }

        /// <summary>
        /// an array of objects representing DNSKEY records
        /// </summary>
        [JsonPropertyName("keyData")]
        public IReadOnlyList<RdapDnsKeyRecord> DnsKeyRecords { get; set; }
    }
}
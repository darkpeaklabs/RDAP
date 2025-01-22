using DarkPeakLabs.Rdap.Values.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Class for domain variant object
    /// </summary>
    public class RdapDomainVariant
    {
        /// <summary>
        /// an array of strings, with each string denoting the relationship between the variants and the containing domain object
        /// </summary>
        [JsonPropertyName("relation")]
        public IReadOnlyCollection<RdapDomainVariantRelation> Relation { get; set; }

        /// <summary>
        /// the name of the Internationalized Domain Name (IDN) table of codepoints, such as one listed with the IANA (see http://www.iana.org/domains/idn-tables)
        /// </summary>
        [JsonPropertyName("idnTable")]
        public string IDNTable { get; set; }

        /// <summary>
        /// an array of variant name objects
        /// </summary>
        [JsonPropertyName("variantNames")]
        public IReadOnlyCollection<RdapDomainVariantName> VariantNames { get; set; }
    }
}
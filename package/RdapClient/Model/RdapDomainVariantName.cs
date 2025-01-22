using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// class for domain variant name
    /// </summary>
    public class RdapDomainVariantName
    {
        /// <summary>
        /// a string containing the LDH name of the named object
        /// </summary>
        [JsonPropertyName("ldhName")]
        public string LDHName { get; set; }

        /// <summary>
        /// a string containing a DNS Unicode name of the named object
        /// </summary>
        [JsonPropertyName("unicodeName")]
        public string UnicodeName { get; set; }
    }
}
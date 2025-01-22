using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Base class for RDAP named (with ldhName and unicodeName properties) objects
    /// </summary>
    public class RdapNamedObjectBase : RdapObjectBase
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
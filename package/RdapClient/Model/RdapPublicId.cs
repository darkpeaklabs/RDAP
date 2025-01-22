using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// This data structure maps a public identifier to an object class
    /// </summary>
    public class RdapPublicId
    {
        /// <summary>
        /// a string denoting the type of public identifier
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// a public identifier of the type denoted by "type"
        /// </summary>
        [JsonPropertyName("identifier")]
        public string Identifier { get; set; }
    }
}
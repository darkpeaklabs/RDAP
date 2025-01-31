using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Interface for RDAP response
    /// </summary>
    public interface IRdapResponse
    {
        /// <summary>
        /// an array of strings, each providing a hint as to the specifications used in the construction of the response
        /// </summary>
        [JsonPropertyName("rdapConformance")]
        IReadOnlyList<string> Conformance { get; set; }

        /// <summary>
        /// List of notices
        /// </summary>
        [JsonPropertyName("notices")]
        IReadOnlyList<RdapNotice> Notices { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [JsonPropertyName("lang")]
        string Language { get; set; }
    }
}

namespace DarkPeakLabs.Rdap
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// class representing response to /autnum lookup request
    /// </summary>
    public class RdapAutnumLookupResponse : RdapAutnum, IRdapResponse
    {
        // IRdapResponse interface

        /// <summary>
        /// an array of strings, each providing a hint as to the specifications used in the construction of the response
        /// </summary>
        [JsonPropertyName("rdapConformance")]
        public IReadOnlyCollection<string> Conformance { get; set; }

        /// <summary>
        /// List of notices
        /// </summary>
        [JsonPropertyName("notices")]
        public IReadOnlyCollection<RdapNotice> Notices { get; set; }

        /// <summary>
        /// Language
        /// </summary>
        [JsonPropertyName("lang")]
        public string Language { get; set; }
    }
}
using DarkPeakLabs.Rdap.Values;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// data structure to signify links to other resources on the Internet
    /// </summary>
    public class RdapLink
    {
        /// <summary>
        /// the context URI as described by [RFC5988]
        /// </summary>
        [JsonPropertyName("value")]
        public string Value { get; set; }

        /// <summary>
        /// relation-types
        /// </summary>
        [JsonPropertyName("rel")]
        public RdapLinkRelationType? Relation { get; set; }

        /// <summary>
        /// href
        /// </summary>
        [JsonPropertyName("href")]
        public string Href { get; set; }

        /// <summary>
        /// a hint indicating what the language of the result of dereferencing the link should be
        /// </summary>
        [JsonPropertyName("hreflang")]
        public string HrefLang { get; set; }

        /// <summary>
        /// label the destination of a link such that it can be used as a human-readable identifier
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// indicates intended destination medium or media for style information
        /// </summary>
        [JsonPropertyName("media")]
        public string Media { get; set; }

        /// <summary>
        /// a hint indicating what the media type of the result of dereferencing the link should be
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
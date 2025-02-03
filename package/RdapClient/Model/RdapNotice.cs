using DarkPeakLabs.Rdap.Values;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// Notice class denotes information about the service providing RDAP information and/or information about the entire response
    /// </summary>
    public class RdapNotice
    {
        /// <summary>
        /// The title of the object
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// string denoting a registered type of remark or notice (see https://www.rfc-editor.org/rfc/rfc7483#section-10.2.1 or https://www.iana.org/assignments/rdap-json-values/rdap-json-values.xhtml)
        /// </summary>
        [JsonPropertyName("type")]
        public RdapNoticeAndRemarkType? Type { get; set; }

        /// <summary>
        /// strings for the purposes of conveying any descriptive text
        /// </summary>
        [JsonPropertyName("description")]
        public IReadOnlyList<string> Description { get; set; }

        /// <summary>
        /// Array of links
        /// </summary>
        [JsonPropertyName("links")]
        public IReadOnlyList<RdapLink> Links { get; set; }
    }
}

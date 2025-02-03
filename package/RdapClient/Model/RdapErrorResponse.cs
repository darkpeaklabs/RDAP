using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// class for error response
    /// </summary>
    public class RdapErrorResponse : IRdapResponse
    {
        /// <summary>
        /// error code number (corresponding to the HTTP response code)
        /// </summary>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// error title
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// error description
        /// </summary>
        [JsonPropertyName("description")]

        //IRespose interface
        public IReadOnlyList<string> Description { get; set; }

        public IReadOnlyList<string> Conformance { get; set; }
        public IReadOnlyList<RdapNotice> Notices { get; set; }
        public string Language { get; set; }
    }
}
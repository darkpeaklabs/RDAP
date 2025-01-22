namespace DarkPeakLabs.Rdap
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Autonomous System number (autnum) object class models Autonomous System number registrations found in RIRs
    /// </summary>
    public class RdapAutnum : RdapObjectBase
    {
        /// <summary>
        /// a number representing the starting number [RFC5396] in the block of Autonomous System numbers
        /// </summary>
        [JsonPropertyName("startAutnum")]
        public int? StartAutnum { get; set; }

        /// <summary>
        /// a number representing the ending number [RFC5396] in the block of Autonomous System numbers
        /// </summary>
        [JsonPropertyName("endAutnum")]
        public int? EndAutnum { get; set; }

        /// <summary>
        /// an identifier assigned to the autnum registration by the registration holder
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// a 0string containing an RIR-specific classification of the autnum
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; }

        /// <summary>
        /// a string containing the name of the two-character country code of the autnum
        /// </summary>
        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
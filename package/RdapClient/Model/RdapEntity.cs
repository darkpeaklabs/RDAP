using DarkPeakLabs.Rdap.Values;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// This object class represents the information of organizations, corporations, governments, non-profits, clubs, individual persons, and informal groups of people.
    /// </summary>
    public class RdapEntity : RdapObjectBase
    {
        /// <summary>
        /// a jCard with the entity's contact information
        /// <see cref="https://tools.ietf.org/html/rfc7483"/>
        /// </summary>
        [JsonPropertyName("vcardArray")]
        public RdapContact Contact { get; set; }

        /// <summary>
        /// an array of strings, each signifying the relationship an object would have with its closest containing object
        /// </summary>
        [JsonPropertyName("roles")]
        public IReadOnlyList<RdapEntityRole> Roles { get; set; }

        /// <summary>
        /// List of public identifiers to an object class
        /// </summary>
        [JsonPropertyName("publicIds")]
        public IReadOnlyList<RdapPublicId> PublicIds { get; set; }

        /// <summary>
        /// this data structure takes the same form as the events data structure(see Section 4.5), but each object in the array MUST NOT have an "eventActor" member.These objects denote
        /// that the entity is an event actor for the given events.  See Appendix B regarding the various ways events can be modeled
        /// </summary>
        [JsonPropertyName("asEventActor")]
        public IReadOnlyList<RdapEvent> AsEventActor { get; set; }

        /// <summary>
        /// an array of IP network objects
        /// </summary>
        [JsonPropertyName("networks")]
        public IReadOnlyList<RdapIPNetwork> Networks { get; set; }

        /// <summary>
        /// an array of autnum network objects
        /// </summary>
        [JsonPropertyName("autnums")]
        public IReadOnlyList<RdapAutnum> Autnums { get; set; }
    }
}

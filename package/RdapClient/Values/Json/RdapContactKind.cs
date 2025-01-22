using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values.Json
{
    /// <summary>
    /// The kind of the entity the Contact represents
    /// </summary>
    public enum RdapContactKind
    {
        /// <summary>
        /// Unknown value
        /// </summary>
        [Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
        Unknown = -1,

        /// <summary>
        /// A single person
        /// </summary>
        Individual,

        /// <summary>
        /// a group of individuals
        /// </summary>
        Group,

        /// <summary>
        /// An organization
        /// </summary>
        Organization,

        /// <summary>
        /// A named location
        /// </summary>
        Location
    }
}
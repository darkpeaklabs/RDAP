using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values.Json
{
    /// <summary>
    /// This identifies the type of RDAP object
    /// </summary>
    public enum RdapObjectClass
    {
        /// <summary>
        /// Unknown value
        /// </summary>
        [Display(Name = "Unknown", Description = "Server returned invalid value")]
        Unknown = -1,

        [Display(Name = "Entity")]
        Entity,

        [Display(Name = "Name Server")]
        NameServer,

        [Display(Name = "Domain")]
        Domain,

        [Display(Name = "IP Network")]
        IpNetwork,
    }
}
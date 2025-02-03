using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values
{
    /// <summary>
    /// The relationship between the variants and the containing domain object
    /// The following values have been registered in the "RDAP JSON Values" registry
    /// https://www.iana.org/assignments/rdap-json-values/rdap-json-values.xhtml
    /// </summary>
    public enum RdapDomainVariantRelation
    {
        /// <summary>
        /// Unknown value
        /// </summary>
        [Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
        Unknown = -1,

        /// <summary>
        /// The variant names are registered in the registry.
        /// </summary>
        Registered,

        /// <summary>
        /// The variant names are not found in the registry.
        /// </summary>
        Unregistered,

        /// <summary>
        /// Registration of the variant names is restricted to certain parties or within certain rules.
        /// </summary>
        RegistrationRestricted,

        /// <summary>
        /// Registration of the variant names is available to generally qualified registrants.
        /// </summary>
        OpenRegistration,

        /// <summary>
        /// Registration of the variant names occurs automatically with the registration of the containing domain registration.
        /// </summary>
        Conjoined
    }
}
// Generated file from IANA registry (2/20/2022 10:53:44 AM)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values
{
    public enum RdapEntityRole
    {
        /// <summary>
        /// Unknown value
        /// </summary>
        [Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
        Unknown = -1,

        /// <summary>
        /// The entity object instance is the registrant of the registration. In some registries, this is known as a maintainer.
        /// </summary>
        [Display(Name = "Registrant", Description = "The entity object instance is the registrant of the registration. In some registries, this is known as a maintainer.")]
        Registrant,

        /// <summary>
        /// The entity object instance is a technical contact for the registration.
        /// </summary>
        [Display(Name = "Technical", Description = "The entity object instance is a technical contact for the registration.")]
        Technical,

        /// <summary>
        /// The entity object instance is an administrative contact for the registration.
        /// </summary>
        [Display(Name = "Administrative", Description = "The entity object instance is an administrative contact for the registration.")]
        Administrative,

        /// <summary>
        /// The entity object instance handles network abuse issues on behalf of the registrant of the registration.
        /// </summary>
        [Display(Name = "Abuse", Description = "The entity object instance handles network abuse issues on behalf of the registrant of the registration.")]
        Abuse,

        /// <summary>
        /// The entity object instance handles payment and billing issues on behalf of the registrant of the registration.
        /// </summary>
        [Display(Name = "Billing", Description = "The entity object instance handles payment and billing issues on behalf of the registrant of the registration.")]
        Billing,

        /// <summary>
        /// The entity object instance represents the authority responsible for the registration in the registry.
        /// </summary>
        [Display(Name = "Registrar", Description = "The entity object instance represents the authority responsible for the registration in the registry.")]
        Registrar,

        /// <summary>
        /// The entity object instance represents a third party through which the registration was conducted (i.e., not the registry or registrar).
        /// </summary>
        [Display(Name = "Reseller", Description = "The entity object instance represents a third party through which the registration was conducted (i.e., not the registry or registrar).")]
        Reseller,

        /// <summary>
        /// The entity object instance represents a domain policy sponsor, such as an ICANN-approved sponsor.
        /// </summary>
        [Display(Name = "Sponsor", Description = "The entity object instance represents a domain policy sponsor, such as an ICANN-approved sponsor.")]
        Sponsor,

        /// <summary>
        /// The entity object instance represents a proxy for another entity object, such as a registrant.
        /// </summary>
        [Display(Name = "Proxy", Description = "The entity object instance represents a proxy for another entity object, such as a registrant.")]
        Proxy,

        /// <summary>
        /// An entity object instance designated to receive notifications about association object instances.
        /// </summary>
        [Display(Name = "Notifications", Description = "An entity object instance designated to receive notifications about association object instances.")]
        Notifications,

        /// <summary>
        /// The entity object instance handles communications related to a network operations center (NOC).
        /// </summary>
        [Display(Name = "Noc", Description = "The entity object instance handles communications related to a network operations center (NOC).")]
        Noc,
    }
}

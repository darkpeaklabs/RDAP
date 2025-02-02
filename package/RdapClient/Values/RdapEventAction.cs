// Generated file from IANA registry (02/02/2025 10:42:36)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values
{
	public enum RdapEventAction
	{
		/// <summary>
		/// Unknown value
		/// </summary>
		[Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
		Unknown = -1,

		/// <summary>
		/// The object instance was initially registered.
		/// </summary>
		[Display(Name = "Registration", Description = "The object instance was initially registered.")]
		Registration,

		/// <summary>
		/// The object instance was registered subsequently to initial registration.
		/// </summary>
		[Display(Name = "Reregistration", Description = "The object instance was registered subsequently to initial registration.")]
		Reregistration,

		/// <summary>
		/// An action noting when the information in the object instance was last changed.
		/// </summary>
		[Display(Name = "Last Changed", Description = "An action noting when the information in the object instance was last changed.")]
		LastChanged,

		/// <summary>
		/// The object instance has been removed or will be removed at a predetermined date and time from the registry.
		/// </summary>
		[Display(Name = "Expiration", Description = "The object instance has been removed or will be removed at a predetermined date and time from the registry.")]
		Expiration,

		/// <summary>
		/// The object instance was removed from the registry at a point in time that was not predetermined.
		/// </summary>
		[Display(Name = "Deletion", Description = "The object instance was removed from the registry at a point in time that was not predetermined.")]
		Deletion,

		/// <summary>
		/// The object instance was reregistered after having been removed from the registry.
		/// </summary>
		[Display(Name = "Reinstantiation", Description = "The object instance was reregistered after having been removed from the registry.")]
		Reinstantiation,

		/// <summary>
		/// The object instance was transferred from one registrar to another.
		/// </summary>
		[Display(Name = "Transfer", Description = "The object instance was transferred from one registrar to another.")]
		Transfer,

		/// <summary>
		/// The object instance was locked (see the \"locked\" status).
		/// </summary>
		[Display(Name = "Locked", Description = "The object instance was locked (see the \"locked\" status).")]
		Locked,

		/// <summary>
		/// The object instance was unlocked (see the \"locked\" status).
		/// </summary>
		[Display(Name = "Unlocked", Description = "The object instance was unlocked (see the \"locked\" status).")]
		Unlocked,

		/// <summary>
		/// An action noting when the information in the object instance in the RDAP database was last synchronized from the authoritative database (e.g. registry database).
		/// </summary>
		[Display(Name = "Last Update Of RDAP Database", Description = "An action noting when the information in the object instance in the RDAP database was last synchronized from the authoritative database (e.g. registry database).")]
		LastUpdateOfRDAPDatabase,

		/// <summary>
		/// An action noting the expiration date of the object in the registrar system.
		/// </summary>
		[Display(Name = "Registrar Expiration", Description = "An action noting the expiration date of the object in the registrar system.")]
		RegistrarExpiration,

		/// <summary>
		/// Association of phone number represented by this ENUM domain to registrant has expired or will expire at a predetermined date and time.
		/// </summary>
		[Display(Name = "Enum Validation Expiration", Description = "Association of phone number represented by this ENUM domain to registrant has expired or will expire at a predetermined date and time.")]
		EnumValidationExpiration,
	}
}

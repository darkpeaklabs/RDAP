// Generated file from IANA registry (02/02/2025 10:42:36)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values
{
	public enum DnsSecDigestType
	{
		/// <summary>
		/// Unknown value
		/// </summary>
		[Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
		Unknown = -1,

		/// <summary>
		/// SHA-1
		/// </summary>
		[Display(Name = "SHA-1", Description = "SHA-1")]
		SHA1 = 1,

		/// <summary>
		/// SHA-256
		/// </summary>
		[Display(Name = "SHA-256", Description = "SHA-256")]
		SHA256 = 2,

		/// <summary>
		/// GOST R 34.11-94
		/// </summary>
		[Display(Name = "GOST R 34.11-94", Description = "GOST R 34.11-94")]
		GOSTR341194 = 3,

		/// <summary>
		/// SHA-384
		/// </summary>
		[Display(Name = "SHA-384", Description = "SHA-384")]
		SHA384 = 4,

		/// <summary>
		/// GOST R 34.11-2012
		/// </summary>
		[Display(Name = "GOST R 34.11-2012", Description = "GOST R 34.11-2012")]
		GOSTR34112012 = 5,

		/// <summary>
		/// SM3
		/// </summary>
		[Display(Name = "SM3", Description = "SM3")]
		SM3 = 6,
	}
}

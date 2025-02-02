// Generated file from IANA registry (02/02/2025 10:42:36)
using System.ComponentModel.DataAnnotations;
namespace DarkPeakLabs.Rdap.Values
{
	public enum DnsSecAlgorithmType
	{
		/// <summary>
		/// Unknown value
		/// </summary>
		[Display(Name = "Unknown Value", Description = "Server returned value not registered with IANA")]
		Unknown = -1,

		/// <summary>
		/// Delete DS
		/// </summary>
		[Display(Name = "Delete DS", Description = "Delete DS")]
		DeleteDS = 0,

		/// <summary>
		/// RSA/MD5 (DEPRECATED, see 5)
		/// </summary>
		[Display(Name = "RSA/MD5 ", Description = "RSA/MD5 (DEPRECATED, see 5)")]
		RSAMD5 = 1,

		/// <summary>
		/// Diffie-Hellman
		/// </summary>
		[Display(Name = "Diffie-Hellman", Description = "Diffie-Hellman")]
		DiffieHellman = 2,

		/// <summary>
		/// DSA/SHA1
		/// </summary>
		[Display(Name = "DSA/SHA1", Description = "DSA/SHA1")]
		DSASHA1 = 3,

		/// <summary>
		/// RSA/SHA-1
		/// </summary>
		[Display(Name = "RSA/SHA-1", Description = "RSA/SHA-1")]
		RSASHA1 = 5,

		/// <summary>
		/// DSA-NSEC3-SHA1
		/// </summary>
		[Display(Name = "DSA-NSEC3-SHA1", Description = "DSA-NSEC3-SHA1")]
		DSANSEC3SHA1 = 6,

		/// <summary>
		/// RSASHA1-NSEC3-SHA1
		/// </summary>
		[Display(Name = "RSASHA1-NSEC3-SHA1", Description = "RSASHA1-NSEC3-SHA1")]
		RSASHA1NSEC3SHA1 = 7,

		/// <summary>
		/// RSA/SHA-256
		/// </summary>
		[Display(Name = "RSA/SHA-256", Description = "RSA/SHA-256")]
		RSASHA256 = 8,

		/// <summary>
		/// RSA/SHA-512
		/// </summary>
		[Display(Name = "RSA/SHA-512", Description = "RSA/SHA-512")]
		RSASHA512 = 10,

		/// <summary>
		/// GOST R 34.10-2001 (DEPRECATED)
		/// </summary>
		[Display(Name = "GOST R 34.10-2001 ", Description = "GOST R 34.10-2001 (DEPRECATED)")]
		GOSTR34102001 = 12,

		/// <summary>
		/// ECDSA Curve P-256 with SHA-256
		/// </summary>
		[Display(Name = "ECDSA Curve P-256 With SHA-256", Description = "ECDSA Curve P-256 with SHA-256")]
		ECDSACurveP256WithSHA256 = 13,

		/// <summary>
		/// ECDSA Curve P-384 with SHA-384
		/// </summary>
		[Display(Name = "ECDSA Curve P-384 With SHA-384", Description = "ECDSA Curve P-384 with SHA-384")]
		ECDSACurveP384WithSHA384 = 14,

		/// <summary>
		/// Ed25519
		/// </summary>
		[Display(Name = "Ed25519", Description = "Ed25519")]
		Ed25519 = 15,

		/// <summary>
		/// Ed448
		/// </summary>
		[Display(Name = "Ed448", Description = "Ed448")]
		Ed448 = 16,

		/// <summary>
		/// SM2 signing algorithm with SM3 hashing algorithm
		/// </summary>
		[Display(Name = "SM2 Signing Algorithm With SM3 Hashing Algorithm", Description = "SM2 signing algorithm with SM3 hashing algorithm")]
		SM2SigningAlgorithmWithSM3HashingAlgorithm = 17,

		/// <summary>
		/// GOST R 34.10-2012
		/// </summary>
		[Display(Name = "GOST R 34.10-2012", Description = "GOST R 34.10-2012")]
		GOSTR34102012 = 23,
#pragma warning disable CA1700

		/// <summary>
		/// Reserved for Indirect Keys
		/// </summary>
		[Display(Name = "Reserved For Indirect Keys", Description = "Reserved for Indirect Keys")]
		ReservedForIndirectKeys = 252,
#pragma warning restore CA1700

		/// <summary>
		/// private algorithm
		/// </summary>
		[Display(Name = "Private Algorithm", Description = "private algorithm")]
		PrivateAlgorithm = 253,

		/// <summary>
		/// private algorithm OID
		/// </summary>
		[Display(Name = "Private Algorithm OID", Description = "private algorithm OID")]
		PrivateAlgorithmOID = 254,
	}
}

using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values.DnsSec
{
    /// <summary>
    /// the algorithm of the DNSKEY RR referred to by the DS record
    /// https://tools.ietf.org/html/rfc4034#appendix-A.1
    /// </summary>
    ///

    /// <summary>
    ///   DNSSEC algorithm type
    /// </summary>
    public enum AlgorithmType
    {
        Unknown,

        /// <summary>
        ///   <para>RSA MD5</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc3110">RFC 3110</see>
        ///     and
        ///     <see cref="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "RSA MD5")]
        RsaMd5 = 1,

        /// <summary>
        ///   <para>Diffie Hellman</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc2539">RFC 2539</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Diffie-Hellman")]
        DiffieHellman = 2,

        /// <summary>
        ///   <para>DSA/SHA-1</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc3755">RFC 3755</see>
        ///     and
        ///     <see cref="http://tools.ietf.org/html/rfc2536">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "DSA/SHA-1")]
        Dsa = 3,

        /// <summary>
        ///   <para>RSA/SHA-1</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc3110">RFC 3110</see>
        ///     and
        ///     <see cref="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "RSA/SHA-1")]
        RsaSha1 = 5,

        /// <summary>
        ///   <para>DSA/SHA-1 using NSEC3 hashs</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc5155">RFC 5155</see>
        ///   </para>
        /// </summary>
        [Display(Name = "DSA/SHA-1 using NSEC3 hashs")]
        DsaNsec3Sha1 = 6,

        /// <summary>
        ///   <para>RSA/SHA-1 using NSEC3 hashs</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc5155">RFC 5155</see>
        ///   </para>
        /// </summary>
        [Display(Name = "RSA/SHA-1 using NSEC3 hashs")]
        RsaSha1Nsec3Sha1 = 7,

        /// <summary>
        ///   <para>RSA/SHA-256</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc5702">RFC 5702</see>
        ///   </para>
        /// </summary>
        [Display(Name = "RSA/SHA-256")]
        RsaSha256 = 8,

        /// <summary>
        ///   <para>RSA/SHA-512</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc5702">RFC 5702</see>
        ///   </para>
        /// </summary>
        [Display(Name = "RSA/SHA-512")]
        RsaSha512 = 10,

        /// <summary>
        ///   <para>GOST R 34.10-2001</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc5933">RFC 5933</see>
        ///   </para>
        /// </summary>
        [Display(Name = "GOST R 34.10-2001")]
        EccGost = 12,

        /// <summary>
        ///   <para>ECDSA Curve P-256 with SHA-256</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc6605">RFC 6605</see>
        ///   </para>
        /// </summary>
        [Display(Name = "ECDSA Curve P-256 with SHA-256")]
        EcDsaP256Sha256 = 13,

        /// <summary>
        ///   <para>ECDSA Curve P-384 with SHA-384</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc6605">RFC 6605</see>
        ///   </para>
        /// </summary>
        [Display(Name = "ECDSA Curve P-384 with SHA-384")]
        EcDsaP384Sha384 = 14,

        /// <summary>
        ///   <para>Indirect</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Indirect")]
        Indirect = 252,

        /// <summary>
        ///   <para>Private key using named algorithm</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Private key using named algorithm")]
        PrivateDns = 253,

        /// <summary>
        ///   <para>Private key using algorithm object identifier</para>
        ///   <para>
        ///     Defined in
        ///     <see cref="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Private key using algorithm object identifier")]
        PrivateOid = 254,
    }
}
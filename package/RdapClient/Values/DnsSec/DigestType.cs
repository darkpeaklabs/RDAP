using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values.DnsSec
{
    /// <summary>
    ///   Type of DNSSEC digest
    /// </summary>
    public enum DigestType
    {
        Unknown = -1,
        None = 0,

        /// <summary>
        ///   <para>SHA-1</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc3658">RFC 3658</see>
        ///   </para>
        /// </summary>
        [Display(Name = "SHA-1")]
        Sha1 = 1,

        /// <summary>
        ///   <para>SHA-256</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc4509">RFC 4509</see>
        ///   </para>
        /// </summary>
        [Display(Name = "SHA-256")]
        Sha256 = 2,

        /// <summary>
        ///   <para>GOST R 34.11-94</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc5933">RFC 5933</see>
        ///   </para>
        /// </summary>
        [Display(Name = "GOST R 34.11-94")]
        EccGost = 3,

        /// <summary>
        ///   <para>SHA-384</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc6605">RFC 6605</see>
        ///   </para>
        /// </summary>
        [Display(Name = "SHA-384")]
        Sha384 = 4,
    }
}
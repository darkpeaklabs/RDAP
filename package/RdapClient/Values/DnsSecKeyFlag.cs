using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values
{
#pragma warning disable CA1711
    public enum DnsSecKeyFlag
#pragma warning restore CA1711
    {
        Unknown = -1,

        /// <summary>
        ///   <para>ZONE</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc3755">RFC 3755</see>
        ///     and
        ///     <see href="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Zone")]
        Zone = 256,

        /// <summary>
        ///   <para>REVOKE</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc5011">RFC 5011</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Revoke")]
        Revoke = 128,

        /// <summary>
        ///   <para>Secure Entry Point (SEP)</para>
        ///   <para>
        ///     Defined in
        ///     <see href="http://tools.ietf.org/html/rfc3755">RFC 3755</see>
        ///     and
        ///     <see href="http://tools.ietf.org/html/rfc4034">RFC 4034</see>
        ///   </para>
        /// </summary>
        [Display(Name = "Secure Entry Point (SEP)")]
        SecureEntryPoint = 1
    }
}

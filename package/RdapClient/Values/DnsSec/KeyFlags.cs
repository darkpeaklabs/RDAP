using System;
using System.ComponentModel.DataAnnotations;

namespace DarkPeakLabs.Rdap.Values.DnsSec
{
    [Flags]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
#pragma warning disable CA2217 // Do not mark enums with FlagsAttribute
    public enum DnsKeyFlags : int
#pragma warning restore CA2217 // Do not mark enums with FlagsAttribute
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
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
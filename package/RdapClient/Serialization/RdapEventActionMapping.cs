using DarkPeakLabs.Rdap.Values;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapEventActionMapping
    {
        /// <summary>
        /// Maps unregistered event action value seen in the field to IANA RDAP JSON values.
        /// <see cref="https://tools.ietf.org/html/draft-blanchet-regext-rdap-deployfindings-05">Draft: RDAP Deployment Findings and Update</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapEventAction result)
        {
            result = value.ToUpperInvariant() switch
            {
                "LAST UPDATE" => RdapEventAction.LastChanged,
                _ => RdapEventAction.Unknown,
            };

            return result != RdapEventAction.Unknown;
        }
    }
}

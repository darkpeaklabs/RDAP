using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapRoleMapping
    {
        /// <summary>
        /// Maps unregistered role value seen in the field to IANA RDAP JSON values.
        /// <see cref="https://tools.ietf.org/html/draft-blanchet-regext-rdap-deployfindings-05">Draft: RDAP Deployment Findings and Update</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapEntityRole result, ILogger logger = null)
        {
            switch (value)
            {
                case "owner":
                    result = RdapEntityRole.Registrant;
                    break;

                default:
                    result = RdapEntityRole.Unknown;
                    break;
            };

            if (result != RdapEntityRole.Unknown)
            {
                logger?.LogDebug("Entity role string value {Value} mapped to value {Enum}", value, result);
            }
            else
            {
                logger?.LogWarning("Unable to map Entity role string value {Value} to RDAP JSON value", value);
            }

            return result != RdapEntityRole.Unknown;
        }
    }
}
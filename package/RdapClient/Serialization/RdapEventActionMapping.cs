using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapEventActionMapping
    {
        /// <summary>
        /// Maps unregistered event action value seen in the field to IANA RDAP JSON values.
        /// <see cref="https://tools.ietf.org/html/draft-blanchet-regext-rdap-deployfindings-05">Draft: RDAP Deployment Findings and Update</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapEventAction result, ILogger logger = null)
        {
            switch (value.ToUpperInvariant())
            {
                case "LAST UPDATE":
                    result = RdapEventAction.LastChanged;
                    break;

                default:
                    result = RdapEventAction.Unknown;
                    break;
            };

            if (result != RdapEventAction.Unknown)
            {
                logger?.LogDebug("Action string value {Value} mapped to value {Enum}", value, result);
            }
            else
            {
                logger?.LogWarning("Unable to map Action string value {Value} to RDAP JSON value", value);
            }

            return result != RdapEventAction.Unknown;
        }
    }
}
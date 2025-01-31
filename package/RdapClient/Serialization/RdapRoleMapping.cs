using DarkPeakLabs.Rdap.Values;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapRoleMapping
    {
        /// <summary>
        /// Maps unregistered role value seen in the field to IANA RDAP JSON values.
        /// <see cref="https://tools.ietf.org/html/draft-blanchet-regext-rdap-deployfindings-05">Draft: RDAP Deployment Findings and Update</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapEntityRole result)
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

            return result != RdapEntityRole.Unknown;
        }
    }
}

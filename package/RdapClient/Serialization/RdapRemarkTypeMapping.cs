using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapRemarkTypeMapping
    {
        /// <summary>
        /// Maps unregistered remark and notice type values seen in the field to IANA RDAP JSON values.
        /// <see cref="https://tools.ietf.org/html/draft-blanchet-regext-rdap-deployfindings-05">Draft: RDAP Deployment Findings and Update</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapNoticeAndRemarkType result, ILogger logger = null)
        {
            switch (value.ToUpperInvariant())
            {
                case "OBJECT TRUNCATED DUE TO SERVER POLICY":
                    result = RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization;
                    break;

                case "RESPONSE TRUNCATED DUE TO AUTHORIZATION":
                    result = RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization;
                    break;

                case "OBJECT TRUNCATED DUE TO AUTHORIZATION":
                    result = RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization;
                    break;

                case "OBJECT REDACTED DUE TO AUTHORIZATION":
                    result = RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization;
                    break;

                default:
                    result = RdapNoticeAndRemarkType.Unknown;
                    break;
            };

            if (result != RdapNoticeAndRemarkType.Unknown)
            {
                logger?.LogDebug("Remark/Notice type string value {Value} mapped to value {Enum}", value, result);
            }
            else
            {
                logger?.LogWarning("Unable to map Remark/Notice string value {Value} to RDAP JSON value", value);
            }

            return result != RdapNoticeAndRemarkType.Unknown;
        }
    }
}
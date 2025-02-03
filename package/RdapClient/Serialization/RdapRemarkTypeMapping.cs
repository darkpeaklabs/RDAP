using DarkPeakLabs.Rdap.Values;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapRemarkTypeMapping
    {
        /// <summary>
        /// Maps unregistered remark and notice type values seen in the field to IANA RDAP JSON values.
        /// <see cref="https://tools.ietf.org/html/draft-blanchet-regext-rdap-deployfindings-05">Draft: RDAP Deployment Findings and Update</see>
        /// </summary>
        public static bool TryMapToRdap(string value, out RdapNoticeAndRemarkType result)
        {
            result = value.ToUpperInvariant() switch
            {
                "OBJECT TRUNCATED DUE TO SERVER POLICY" => RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization,
                "RESPONSE TRUNCATED DUE TO AUTHORIZATION" => RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization,
                "OBJECT TRUNCATED DUE TO AUTHORIZATION" => RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization,
                "OBJECT REDACTED DUE TO AUTHORIZATION" => RdapNoticeAndRemarkType.ObjectTruncatedDueToAuthorization,
                _ => RdapNoticeAndRemarkType.Unknown,
            };
 
            return result != RdapNoticeAndRemarkType.Unknown;
        }
    }
}

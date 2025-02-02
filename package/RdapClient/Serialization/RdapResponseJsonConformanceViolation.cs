using System.Text.Json.Nodes;
using DarkPeakLabs.Rdap.Conformance;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapResponseJsonConformanceViolation : RdapConformanceViolation
    {
        public JsonNode JsonNode { get; }

        internal RdapResponseJsonConformanceViolation(
            RdapConformanceViolationSeverity severity,
            RdapConformanceViolationCategory category,
            string message,
            JsonNode node):
            base(severity, category, message)
        {
            JsonNode = node;
        }
    }
}

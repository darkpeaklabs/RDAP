using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Conformance
{
    public class RdapJsonConformanceViolation : RdapConformanceViolation
    {
        public JsonNode JsonNode { get; }

        internal RdapJsonConformanceViolation(
            RdapConformanceViolationSeverity severity,
            RdapConformanceViolationCategory category,
            string message,
            JsonNode node) :
            base(severity, category, message)
        {
            JsonNode = node;
        }
    }
}

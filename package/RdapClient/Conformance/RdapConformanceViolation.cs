namespace DarkPeakLabs.Rdap.Conformance
{
    /// <summary>
    /// Class representing RDAP conformance violation
    /// </summary>
    public class RdapConformanceViolation
    {
        internal RdapConformanceViolation(RdapConformanceViolationSeverity severity, RdapConformanceViolationCategory category, string message)
        {
            Severity = severity;
            Category = category;
            Issue = message;
        }

        /// <summary>
        /// Conformance violation category
        /// </summary>
        public RdapConformanceViolationCategory Category { get; }

        /// <summary>
        /// Conformance violation severity
        /// </summary>
        public RdapConformanceViolationSeverity Severity { get; }

        /// <summary>
        /// Conformance violation message
        /// </summary>
        public string Issue { get; }
    }
}

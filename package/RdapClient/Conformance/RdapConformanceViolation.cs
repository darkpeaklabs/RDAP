namespace DarkPeakLabs.Rdap.Conformance
{
    /// <summary>
    /// Class representing RDAP conformance violation
    /// </summary>
    public class RdapConformanceViolation
    {
        /// <summary>
        /// Conformance violation category
        /// </summary>
        public RdapConformanceViolationCategory Category { get; set; }

        /// <summary>
        /// Conformance violation severity
        /// </summary>
        public RdapConformanceViolationSeverity Severity { get; set; }

        /// <summary>
        /// Conformance violation message
        /// </summary>
        public string Issue { get; set; }
    }
}
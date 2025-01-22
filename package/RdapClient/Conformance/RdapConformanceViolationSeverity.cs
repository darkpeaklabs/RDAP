namespace DarkPeakLabs.Rdap.Conformance
{
    /// <summary>
    /// RDAP conformance violation severity level
    /// </summary>
    public enum RdapConformanceViolationSeverity
    {
        /// <summary>
        /// Information about possible conformance violation
        /// </summary>
        Information,

        /// <summary>
        /// Warning about conformance violation
        /// </summary>
        Warning,

        /// <summary>
        /// Critical conformance violation
        /// </summary>
        Error
    }
}
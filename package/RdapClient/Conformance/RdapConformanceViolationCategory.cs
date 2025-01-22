namespace DarkPeakLabs.Rdap.Conformance
{
    /// <summary>
    /// RDAP Conformance violation category
    /// </summary>
    public enum RdapConformanceViolationCategory
    {
        /// <summary>
        /// Conformance violation related to JSON response
        /// </summary>
        JSON,

        /// <summary>
        /// Conformance violation related to HTTP response
        /// </summary>
        HttpResponse,

        /// <summary>
        /// Conformance violation related to RDAp web service implementation
        /// </summary>
        WebServiceImplementation,
        JsonValidation,
    }
}
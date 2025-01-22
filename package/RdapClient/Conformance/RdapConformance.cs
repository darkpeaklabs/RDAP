using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace DarkPeakLabs.Rdap.Conformance
{
    /// <summary>
    /// Class for collecting RDAp conformance violations
    /// </summary>
    public class RdapConformance
    {
        /// <summary>
        /// Class logger
        /// </summary>
        private ILogger _logger;

        private List<RdapConformanceViolation> violations;

        /// <summary>
        /// List of violations
        /// </summary>
        public IReadOnlyList<RdapConformanceViolation> Violations { get => violations; }

        /// <summary>
        /// Returns true if current instance contains any violations
        /// </summary>
        public bool HasConformanceViolations { get => Violations.Count > 0; }

        /// <summary>
        /// Creates a new RDAP conformance instance
        /// </summary>
        internal RdapConformance(ILogger logger = null)
        {
            _logger = logger;
            violations = new List<RdapConformanceViolation>();
        }

        /// <summary>
        /// Adds new violation to the list
        /// </summary>
        /// <param name="severity">Violation severity</param>
        /// <param name="category">Violation category</param>
        /// <param name="message">Violation message</param>
        public void AddViolation(RdapConformanceViolationSeverity severity, RdapConformanceViolationCategory category, string message)
        {
            switch (severity)
            {
                case RdapConformanceViolationSeverity.Error:
                    _logger?.LogError("Conformance violation. Category = {Category}, Message = {Message}", category, message); break;
                case RdapConformanceViolationSeverity.Information:
                    _logger?.LogInformation("Conformance violation. Category = {Category}, Message = {Message}", category, message); break;
                case RdapConformanceViolationSeverity.Warning:
                    _logger?.LogWarning("Conformance violation. Category = {Category}, Message = {Message}", category, message); break;
            }

            violations.Add(new RdapConformanceViolation()
            {
                Severity = severity,
                Category = category,
                Issue = message
            });
        }

        /// <summary>
        /// Adds new violation to the list
        /// </summary>
        /// <param name="severity">Violation severity</param>
        /// <param name="category">Violation category</param>
        /// <param name="message">Violation message</param>
        /// <param name="format">Message format string</param>
        /// <param name="args">Message arguments</param>
        internal void AddViolation(RdapConformanceViolationSeverity severity, RdapConformanceViolationCategory category, string format, params object[] args) => AddViolation(severity, category, string.Format(CultureInfo.InvariantCulture, format, args));

        /// <summary>
        /// Adds new JSON response violation to the list
        /// </summary>
        /// <param name="severity">Violation severity</param>
        /// <param name="reader">JSON reader</param>
        /// <param name="message">Violation message</param>
        internal void AddJsonViolation(RdapConformanceViolationSeverity severity, ref Utf8JsonReader reader, string message)
        {
            AddViolation(severity, RdapConformanceViolationCategory.JSON, $"JSON start index: {reader.TokenStartIndex}, {message}");
        }

        /// <summary>
        /// Adds new JSON response violation to the list
        /// </summary>
        /// <param name="severity">Violation severity</param>
        /// <param name="reader">JSON reader</param>
        /// <param name="format">Message format string</param>
        /// <param name="args">Message arguments</param>
        internal void AddJsonViolation(RdapConformanceViolationSeverity severity, ref Utf8JsonReader reader, string format, params object[] args) => AddJsonViolation(severity, ref reader, string.Format(CultureInfo.InvariantCulture, format, args));

        /// <summary>
        /// Adds new web service implementation violation to the list
        /// </summary>
        /// <param name="severity">Violation severity</param>
        /// <param name="message">Violation message</param>
        internal void AddImplementationViolation(RdapConformanceViolationSeverity severity, string message)
        {
            AddViolation(severity, RdapConformanceViolationCategory.WebServiceImplementation, message);
        }

        /// <summary>
        /// Adds new web service implementation violation to the list
        /// </summary>
        /// <param name="severity">Violation severity</param>
        /// <param name="format">Message format string</param>
        /// <param name="args">Message arguments</param>
        internal void AddImplementationViolation(RdapConformanceViolationSeverity severity, string format, params object[] args) => AddImplementationViolation(severity, string.Format(CultureInfo.InvariantCulture, format, args));

        public void AddViolations(IReadOnlyList<RdapConformanceViolation> violations)
        {
            this.violations.AddRange(violations);
        }
    }
}
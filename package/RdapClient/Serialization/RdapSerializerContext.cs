using System;
using System.Text.Json.Nodes;
using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapSerializerContext(RdapConformance conformance, ILogger logger)
    {
        public RdapConformance Conformance { get; internal set; } = conformance ?? throw new ArgumentNullException(paramName: nameof(conformance));
        public ILogger Logger { get; internal set; } = logger;

        internal void AddJsonViolation(RdapConformanceViolationSeverity severity, JsonNode node, string message)
        {
            Conformance.AddViolation(severity, RdapConformanceViolationCategory.JSON, $"JSON path: {node.GetPath()}, {message}");
        }

        internal void AddJsonViolation(RdapConformanceViolationSeverity severity, string message)
        {
            Conformance.AddViolation(severity, RdapConformanceViolationCategory.JSON, message);
        }

        internal void AddJsonViolationError(JsonNode node, string message)
        {
            AddJsonViolation(RdapConformanceViolationSeverity.Error, node, message);
        }
        internal void AddJsonViolationError(string message)
        {
            AddJsonViolation(RdapConformanceViolationSeverity.Error, message);
        }

        internal void AddJsonViolationWarning(JsonNode node, string message)
        {
            AddJsonViolation(RdapConformanceViolationSeverity.Warning, node, message);
        }

        internal void AddJsonViolationWarning(string message)
        {
            AddJsonViolation(RdapConformanceViolationSeverity.Warning, message);
        }

        internal void LogCritical(string message, params object[] args)
        {
#pragma warning disable CA2254 // Template should be a static expression
            Logger?.LogCritical(message, args);
#pragma warning restore CA2254 // Template should be a static expression
        }

        internal void LogTrace(string message, params object[] args)
        {
            if (Logger == null)
            {
                return;
            }

            if (Logger.IsEnabled(LogLevel.Trace))
            {
#pragma warning disable CA2254 // Template should be a static expression
                Logger.LogTrace(message, args);
#pragma warning restore CA2254 // Template should be a static expression
            }
        }
    }
}

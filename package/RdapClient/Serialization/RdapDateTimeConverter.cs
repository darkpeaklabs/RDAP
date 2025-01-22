using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json converter for object handle
    /// </summary>
    internal class RdapDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly string[] formats = new string[] {
            "yyyy-MM-ddTHH:mm:ss.FFF",
            "yyyy-MM-ddTHH:mm:ss.FFFZ",
            "yyyy-MM-ddTHH:mm:ss.FFFz",
            "yyyy-MM-ddTHH:mm:ss.FFFzz",
            "yyyy-MM-ddTHH:mm:ss.FFFzzz",
            "yyyy-MM-ddTHH:mm:ss.FFFzzzz"
        };

        /// <summary>
        /// Class logger
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// RDAP conformance
        /// </summary>
        private readonly RdapConformance conformance;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="errors"></param>
        public RdapDateTimeConverter(RdapConformance conformance, ILogger logger = null)
        {
            _logger = logger;
            this.conformance = conformance;
        }

        /// <summary>
        /// Reads and parses object handle value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value = reader.GetString();
            _logger?.LogDebug("Parsing string value {Value} to DateTime", value);

            DateTime dt;

            if (DateTime.TryParse(value, out dt))
            {
                return dt;
            }

            foreach (string format in formats)
            {
                if (DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out dt))
                {
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"Invalid date and time value {value}. Using alternative format \"{format}\".");
                    return dt;
                }
            }

            conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"Invalid date and time value {value}");

            if (RdapSerializer.ThrowOnError)
            {
                _logger?.LogCritical("Unable to parse string value {Value} to DateTime", value);
                throw new RdapJsonException($"Unable to parse string value \"{value}\" to DateTime", ref reader);
            }
            else
            {
                _logger?.LogWarning("Unable to parse string value {Value} to DateTime. Using DateTime.MinValue", value);
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
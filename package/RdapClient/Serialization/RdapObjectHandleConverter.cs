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
    internal class RdapObjectHandleConverter : JsonConverter<RdapObjectHandle>
    {
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
        public RdapObjectHandleConverter(RdapConformance conformance, ILogger logger = null)
        {
            this.conformance = conformance;
            _logger = logger;
        }

        /// <summary>
        /// Reads and parses object handle value
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override RdapObjectHandle Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string value;
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    value = reader.GetString();
                    break;

                case JsonTokenType.Number:
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Object handle should be a string. Response contains a number.");
                    value = reader.GetInt32().ToString(CultureInfo.InvariantCulture);
                    break;

                default:
                    throw new RdapJsonException($"Unexpected token type {reader.TokenType} when reading object handle", ref reader);
            }

            string tag = null;

            _logger?.LogDebug("Converting string value {Value} to RDAP object handle", value);

            int tagIndex = value.LastIndexOf('-');

            if ((tagIndex != -1) && (tagIndex + 1 < value.Length))
            {
                tag = value.Substring(tagIndex + 1);
                _logger?.LogDebug("Found object handle tag {Tag}", tag);
            }

            return new RdapObjectHandle()
            {
                Value = value,
                Tag = tag
            };
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, RdapObjectHandle value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
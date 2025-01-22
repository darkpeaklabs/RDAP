using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapStringConverter : JsonConverter<string>
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
        public RdapStringConverter(RdapConformance conformance, ILogger logger = null)
        {
            this.conformance = conformance;
            _logger = logger;
        }

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return reader.GetString();

                case JsonTokenType.Number:
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Expected data type of JSON value is string, found a number instead.");
                    return reader.GetInt32().ToString(CultureInfo.InvariantCulture);

                case JsonTokenType.StartArray:
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Expected data type of JSON value is string, found an array instead.");
                    reader.Skip();
                    return null;

                default:
                    throw new RdapJsonException($"Unexpected token type {reader.TokenType} when reading string", ref reader);
            }
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

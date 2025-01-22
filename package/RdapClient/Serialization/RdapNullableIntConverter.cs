using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapNullableIntConverter : JsonConverter<int?>
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
        public RdapNullableIntConverter(RdapConformance conformance, ILogger logger = null)
        {
            this.conformance = conformance;
            _logger = logger;
        }

        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    string str = reader.GetString();
                    if (int.TryParse(str, out int num))
                    {
                        conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Found a string instead of a number.");
                        return num;
                    }
                    else
                    {
                        conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, "Found a non-numeric string instead of a number.");
                        return null;
                    }

                case JsonTokenType.Number:
                    return reader.GetInt32();

                default:
                    throw new RdapJsonException($"Unexpected token type {reader.TokenType} when reading a number", ref reader);
            }
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

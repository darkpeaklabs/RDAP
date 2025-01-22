using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapNullableBooleanConverter : JsonConverter<bool?>
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
        public RdapNullableBooleanConverter(RdapConformance conformance, ILogger logger = null)
        {
            this.conformance = conformance;
            _logger = logger;
        }

        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    string str = reader.GetString();
                    if (bool.TryParse(str, out bool value))
                    {
                        conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Found a string instead of a boolean.");
                        return value;
                    }
                    else
                    {
                        conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, "Found an invalid string instead of a boolean value.");
                        return null;
                    }

                case JsonTokenType.False:
                    return false;

                case JsonTokenType.True:
                    return false;

                default:
                    throw new RdapJsonException($"Unexpected token type {reader.TokenType} when reading a number", ref reader);
            }
        }

        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

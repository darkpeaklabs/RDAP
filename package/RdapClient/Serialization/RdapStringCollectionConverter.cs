using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapStringCollectionConverter : JsonConverter<IEnumerable<string>>
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
        public RdapStringCollectionConverter(RdapConformance conformance, ILogger logger = null)
        {
            this.conformance = conformance;
            _logger = logger;
        }

        public override IEnumerable<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Found single string instead of expected array of strings.");
                    return new string[] { reader.GetString() };

                case JsonTokenType.Number:
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Warning, ref reader, "Found a number instead of expected array of strings");
                    return new string[] { reader.GetInt32().ToString(CultureInfo.InvariantCulture) };

                //case JsonTokenType.StartArray:
                //    List<string> strings = new List<string>();
                //    RdapStringConverter stringConverter = new RdapStringConverter(conformance, _logger);
                //    while (reader.Read())
                //    {
                //        if (reader.TokenType == JsonTokenType.EndArray)
                //        {
                //            break;
                //        }
                //        strings.Add(stringConverter.Read(ref reader, typeof(string), options));
                //    }
                //    return strings;

                default:
                    throw new RdapJsonException($"Unexpected token type {reader.TokenType} when reading list of strings", ref reader);
            }
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<string> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

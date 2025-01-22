using DarkPeakLabs.Rdap.Conformance;
using DarkPeakLabs.Rdap.Values.Json;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json converter for RDAP enum types
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    internal class RdapEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct
    {
        /// <summary>
        /// RDAP conformance
        /// </summary>
        private readonly RdapConformance conformance;

        /// <summary>
        /// Class logger
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"></param>
        public RdapEnumConverter(JsonSerializerOptions options, RdapConformance conformance, ILogger logger = null)
        {
            _ = options ?? throw new ArgumentNullException(paramName: nameof(options));

            this.conformance = conformance;
            _logger = logger;
        }

        /// <summary>
        /// Function converts string token to Enum
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // decide how to parse base on token type
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    return ParseString(reader.GetString(), typeToConvert, ref reader);

                case JsonTokenType.Number:
                    var undelayingType = typeToConvert.GetEnumUnderlyingType();
                    // read token value according to TEnum underlying numeric type
                    if (undelayingType == typeof(int))
                    {
                        if (reader.TryGetInt32(out int value))
                        {
                            return ParseNumber(value, typeToConvert);
                        }
                        conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"Value cannot be read as 32-bit integer for type {typeToConvert.Name}");
                        if (RdapSerializer.ThrowOnError)
                        {
                            throw new RdapJsonException($"Value cannot be read as 32-bit integer for type {typeToConvert.Name}", ref reader);
                        }
                        else
                        {
                            return GetUnknown(typeToConvert);
                        }
                    }
                    else if (undelayingType == typeof(byte))
                    {
                        if (reader.TryGetByte(out byte value))
                        {
                            return ParseNumber(value, typeToConvert);
                        }
                        conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"Value cannot be read as byte for type {typeToConvert.Name}");
                        if (RdapSerializer.ThrowOnError)
                        {
                            throw new RdapJsonException($"Value cannot be read as byte for type {typeToConvert.Name}", ref reader);
                        }
                        else
                        {
                            return GetUnknown(typeToConvert);
                        }
                    }
                    throw new RdapJsonException($"Enum {typeToConvert.Name} has unsupported enum underlaying type {undelayingType.Name}", ref reader);

                default:
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"Unexpected token type {reader.TokenType} parsing response JSON for type {typeToConvert.Name}");
                    if (RdapSerializer.ThrowOnError)
                    {
                        throw new RdapJsonException($"Unexpected token type {reader.TokenType} parsing response JSON for type {typeToConvert.Name}", ref reader);
                    }
                    else
                    {
                        return GetUnknown(typeToConvert);
                    }
            }
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attempts to get well-known enum value Unknown for TEnum
        /// The Unknown value is used to indicate the string value cannot be converted to TEnum
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        private static TEnum GetUnknown(Type typeToConvert)
        {
            // try parse "Unknown" and return it to indicate the string value was not recognized
            if (Enum.TryParse<TEnum>("Unknown", true, out TEnum result))
            {
                return result;
            }

            // if enum does not contain "Unknown", throw an exception
            throw new RdapJsonException($"Enum type {typeToConvert.Name} does not have \"Unknown\" value");
        }

        /// <summary>
        /// Converts JSON string token value to TEnum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        private TEnum ParseString(string value, Type typeToConvert, ref Utf8JsonReader reader)
        {
            TEnum result;
            string conformanceError = $"Value \"{value}\" is not in IANA RDAP JSON Values registry. Type: {typeToConvert.Name}.";

            // Try parse string value and return enum
            if (RdapEnumHelper.TryParseString(value, out result, _logger))
            {
                return result;
            }

            // try to map known unregistered values to RDAP values
            if (typeToConvert == typeof(RdapStatus))
            {
                if (RdapStatusMapping.TryMapToRdap(value, out RdapStatus status, _logger))
                {
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"{conformanceError} Using value according to [RFC 8056].");
                    return (TEnum)(object)status;
                }
            }
            else if (typeToConvert == typeof(RdapNoticeAndRemarkType))
            {
                if (RdapRemarkTypeMapping.TryMapToRdap(value, out RdapNoticeAndRemarkType remarkType, _logger))
                {
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"{conformanceError} Using value {remarkType} according to [draft-blanchet-regext-rdap-deployfindings].");
                    return (TEnum)(object)remarkType;
                }
            }
            else if (typeToConvert == typeof(RdapEventAction))
            {
                if (RdapEventActionMapping.TryMapToRdap(value, out RdapEventAction eventAction, _logger))
                {
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"{conformanceError} Using value {eventAction} according to [draft-blanchet-regext-rdap-deployfindings].");
                    return (TEnum)(object)eventAction;
                }
            }
            else if (typeToConvert == typeof(RdapEntityRole))
            {
                if (RdapRoleMapping.TryMapToRdap(value, out RdapEntityRole role, _logger))
                {
                    conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"{conformanceError} Using value {role} according to [draft-blanchet-regext-rdap-deployfindings].");
                    return (TEnum)(object)role;
                }
            }

            conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, $"{conformanceError}");

            return GetUnknown(typeToConvert);
        }

        /// <summary>
        /// Tries converting object to TEnum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryConvertTo(object value, out TEnum? result)
        {
            try
            {
                result = (TEnum)value;
                return true;
            }
            catch (InvalidCastException)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Converts number to TEnum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        private TEnum ParseNumber(object value, Type typeToConvert)
        {
            if (TryConvertTo(value, out TEnum? result))
            {
                return result.Value;
            }
            _logger?.LogCritical("Value {Value} is not defined for type {Type}.", value, typeToConvert.Name);
            if (RdapSerializer.ThrowOnError)
            {
                throw new RdapJsonException($"Value {value} is not defined for type {typeToConvert.Name}.");
            }
            else
            {
                return GetUnknown(typeToConvert);
            }
        }
    }
}
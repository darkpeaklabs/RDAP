using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using DarkPeakLabs.Rdap.Values;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapEnumConverter
    {
        public static bool TryGetValue(JsonValue jsonValue, Type enumType, RdapSerializerContext context, out object enumValue)
        {
            JsonValueKind valueKind = jsonValue.GetValueKind();

            switch (valueKind)
            {
                case JsonValueKind.String:
                    enumValue = ParseString(jsonValue, enumType, context);
                    return true;

                case JsonValueKind.Number:
                    if (jsonValue.TryGetValue(out int value))
                    {
                        enumValue = ParseNumber(value, enumType, context);
                        return true;
                    }
                    context.AddJsonViolationError(jsonValue, $"Value cannot be read as 32-bit integer for type {enumType.Name}");
                    enumValue = GetUnknown(enumType);
                    return true;

                default:
                    context.AddJsonViolationError(jsonValue, $"Unexpected token type {valueKind} parsing response JSON for type {enumType.Name}");
                    enumValue = GetUnknown(enumType);
                    return true;
            }
        }

        internal static bool TryParseString<T>(string value, out T result) where T : struct
        {
            return Enum.TryParse(
                NormalizeStringValue(value),
                true,
                out result);
        }

        internal static bool TryParseString(string value, Type enumType, out object result)
        {
            return Enum.TryParse(
                enumType,
                NormalizeStringValue(value),
                ignoreCase: true,
                out result);
        }

        private static string NormalizeStringValue(string value)
        {
            return value
                .Replace(" ", "", StringComparison.Ordinal)
                .Replace("-", "", StringComparison.Ordinal)
                .Replace(".", "", StringComparison.Ordinal)
                .Replace("_", "", StringComparison.Ordinal);
        }

        private static object GetUnknown(Type enumType)
        {
            // try parse "Unknown" and return it to indicate the string value was not recognized
            if (Enum.TryParse(enumType, "Unknown", ignoreCase: true, out object result))
            {
                return result;
            }

            if (RdapSerializer.HandleUndefinedEnumValue)
            {
                return Enum.GetValues(enumType);
            }

            // if enum does not contain "Unknown", throw an exception
            throw new RdapSerializerException($"Enum type {enumType.Name} does not have \"Unknown\" value");
        }

        /// <summary>
        /// Converts JSON string token value to TEnum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        private static object ParseString(JsonValue jsonValue, Type enumType, RdapSerializerContext context)
        {
            string value = jsonValue.GetValue<string>();
            string conformanceError = $"Value \"{value}\" is not in IANA RDAP JSON Values registry. Type: {enumType.Name}.";

            // Try parse string value and return enum
            if (TryParseString(value, enumType, out var result))
            {
                return result;
            }

            // try to map known unregistered values to RDAP values
            if (enumType == typeof(RdapStatus))
            {
                if (RdapStatusMapping.TryMapToRdap(value, out RdapStatus status))
                {
                    context.AddJsonViolationError(jsonValue, $"{conformanceError} Using value according to [RFC 8056].");
                    return status;
                }
            }
            else if (enumType == typeof(RdapNoticeAndRemarkType))
            {
                if (RdapRemarkTypeMapping.TryMapToRdap(value, out RdapNoticeAndRemarkType remarkType))
                {
                    context.AddJsonViolationError(jsonValue, $"{conformanceError} Using value {remarkType} according to [draft-blanchet-regext-rdap-deployfindings].");
                    return remarkType;
                }
            }
            else if (enumType == typeof(RdapEventAction))
            {
                if (RdapEventActionMapping.TryMapToRdap(value, out RdapEventAction eventAction))
                {
                    context.AddJsonViolationError(jsonValue, $"{conformanceError} Using value {eventAction} according to [draft-blanchet-regext-rdap-deployfindings].");
                    return eventAction;
                }
            }
            else if (enumType == typeof(RdapEntityRole))
            {
                if (RdapRoleMapping.TryMapToRdap(value, out RdapEntityRole role))
                {
                    context.AddJsonViolationError(jsonValue, $"{conformanceError} Using value {role} according to [draft-blanchet-regext-rdap-deployfindings].");
                    return role;
                }
            }

            context.AddJsonViolationError(jsonValue, $"{conformanceError}");

            return GetUnknown(enumType);
        }

        private static object ParseNumber(int value, Type enumType, RdapSerializerContext context)
        {
            foreach (var enumValue in Enum.GetValues(enumType))
            {
                if ((int)enumValue == value)
                {
                    return enumValue;
                }
            }

            context.LogCritical("Value {Value} is not defined for type {Type}.", value, enumType.Name);
            if (RdapSerializer.HandleUndefinedEnumValue)
            {
                return GetUnknown(enumType);
            }
            else
            {
                throw new RdapSerializerException($"Value {value} is not defined for type {enumType.Name}.");
            }
        }
    }
}

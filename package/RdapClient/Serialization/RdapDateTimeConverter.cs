using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json value converter for date and time
    /// </summary>
    internal static class RdapDateTimeConverter
    {
        private static readonly string[] formats = [
            "yyyy-MM-ddTHH:mm:ss.FFF",
            "yyyy-MM-ddTHH:mm:ss.FFFZ",
            "yyyy-MM-ddTHH:mm:ss.FFFz",
            "yyyy-MM-ddTHH:mm:ss.FFFzz",
            "yyyy-MM-ddTHH:mm:ss.FFFzzz",
            "yyyy-MM-ddTHH:mm:ss.FFFzzzz"
        ];

        public static bool TryGetValue(JsonValue jsonValue, RdapSerializerContext context, out DateTimeOffset dateTime)
        {
            dateTime = default;
            var valueKind = jsonValue.GetValueKind();
            switch (valueKind)
            {
                case JsonValueKind.String:
                    break;
                default:
                    context.AddJsonViolationError(jsonValue, $"{valueKind} is not a valid JSON token type for date and time property value {jsonValue.GetPropertyName()}");
                    return false;
            }

            string value = jsonValue.GetValue<string>();

            if (DateTimeOffset.TryParse(value, out dateTime))
            {
                return true;
            }

            foreach (string format in formats)
            {
                if (DateTimeOffset.TryParseExact(value, format, null, DateTimeStyles.None, out dateTime))
                {
                    context.AddJsonViolationError(jsonValue, $"{value} is not in a valid format for date and time value. Using alternative format \"{format}\".");
                    return true;
                }
            }

            context.AddJsonViolationError(jsonValue, $"{value} is not a valid date and time value");
            return false;
        }
    }
}

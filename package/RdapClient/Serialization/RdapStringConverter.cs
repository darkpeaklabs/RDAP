using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapStringConverter
    {
        public static bool TryGetValue(JsonValue jsonValue, RdapSerializerContext context, out string value)
        {
            value = default;

            var valueKind = jsonValue.GetValueKind();
            switch (valueKind)
            {
                case JsonValueKind.String:
                    value = jsonValue.GetValue<string>();
                    return true;

                case JsonValueKind.Number:
                    context.AddJsonViolationError(jsonValue, $"Property {jsonValue.GetPropertyName()} should be a string not a number");
                    if (jsonValue.TryGetValue<int>(out var intValue))
                    {
                        value = intValue.ToString(CultureInfo.InvariantCulture);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    context.AddJsonViolationError(jsonValue, $"{valueKind} is not a valid JSON token type for string property {jsonValue.GetPropertyName()}");
                    return false;
            }
        }
    }
}

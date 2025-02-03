using System.Text.Json;
using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapBooleanConverter 
    {
        public static bool TryGetValue(JsonValue jsonValue, RdapSerializerContext context, out bool value)
        {
            value = default;

            var valueKind = jsonValue.GetValueKind();
            switch (valueKind)
            {
                case JsonValueKind.String:
                    string str = jsonValue.GetValue<string>();
                    if (bool.TryParse(str, out value))
                    {
                        context.AddJsonViolationWarning(jsonValue, "Found a string instead of a boolean.");
                        return true;
                    }
                    else
                    {
                        context.AddJsonViolationError(jsonValue, "Found an invalid string instead of a boolean value.");
                        return false;
                    }

                case JsonValueKind.False:
                case JsonValueKind.True:
                    value = valueKind == JsonValueKind.True;
                    return true;

                default:
                    context.AddJsonViolationError(jsonValue, $"{valueKind} is not a valid JSON token type for boolean property value {jsonValue.GetPropertyName()}");
                    return false;
            }
        }
    }
}

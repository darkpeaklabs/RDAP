using System.Text.Json;
using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapIntConverter
    {
        public static bool TryGetValue(JsonValue jsonValue, RdapSerializerContext context, out int value)
        {
            value = default;

            var valueKind = jsonValue.GetValueKind();
            switch (valueKind)
            {
                case JsonValueKind.String:
                    string str = jsonValue.GetValue<string>();
                    if (int.TryParse(str, out value))
                    {
                        context.AddJsonViolationError(jsonValue, "Found a string instead of a number.");
                        return true;
                    }
                    else
                    {
                        context.AddJsonViolationError(jsonValue, "Found a non-numeric string instead of a number.");
                        return false;
                    }

                case JsonValueKind.Number:
                    value = jsonValue.GetValue<int>();
                    return true;

                default:
                    context.AddJsonViolationError(jsonValue, $"{valueKind} is not a valid JSON token type for int property {jsonValue.GetPropertyName()}");
                    return false;
            }
        }
    }
}

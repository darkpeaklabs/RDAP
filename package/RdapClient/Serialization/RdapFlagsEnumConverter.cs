using System;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapFlagsEnumConverter
    {
        public static bool TryGetValue(JsonValue jsonValue, Type enumType, RdapSerializerContext context, out object enumValue)
        {
            JsonValueKind valueKind = jsonValue.GetValueKind();
            int value;
            enumValue = Enum.ToObject(enumType, 0);

            switch (valueKind)
            {
                case JsonValueKind.String:
                    if (!int.TryParse(jsonValue.GetValue<string>(), out value))
                    {
                        context.AddJsonViolationError(jsonValue, $"Value is not a number");
                        return false;
                    }
                    break;

                case JsonValueKind.Number:
                    if (!jsonValue.TryGetValue(out value))
                    {
                        context.AddJsonViolationError(jsonValue, $"Value is not an integer");
                        return false;
                    }
                    break; ;

                default:
                    context.AddJsonViolationError(jsonValue, $"Unexpected token type {valueKind} parsing response JSON for type {enumType.Name}");
                    return false;
            }

            int flags = 0;
            foreach(int flagValue in Enum.GetValues(enumType))
            {
                if ((flagValue & value) == flagValue)
                {
                    flags |= flagValue;
                }
            }
            enumValue = Enum.ToObject(enumType, flags);

            return true;
        }
    }
}

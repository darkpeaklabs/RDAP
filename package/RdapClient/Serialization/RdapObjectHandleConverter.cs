using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json converter for object handle
    /// </summary>
    internal class RdapObjectHandleConverter
    {
        public static bool TryGetValue(JsonNode jsonNode, RdapSerializerContext context, out RdapObjectHandle objectHandle)
        {
            objectHandle = default;
            string objectHandleValue;
            JsonValueKind valueKind = jsonNode.GetValueKind();

            switch (valueKind)
            {
                case JsonValueKind.String:
                    objectHandleValue = jsonNode.GetValue<string>();
                    break;

                case JsonValueKind.Number:
                    context.AddJsonViolationWarning(jsonNode, "Object handle should be a string not a number.");
                    objectHandleValue = jsonNode.GetValue<int>().ToString(CultureInfo.InvariantCulture);
                    break;

                default:
                    context.AddJsonViolationError(jsonNode, $"{valueKind} is not a valid JSON token type for object handle property {jsonNode.GetPropertyName()}");
                    return false;
            }

            objectHandle = new RdapObjectHandle()
            {
                Value = objectHandleValue
            };

            int tagIndex = objectHandleValue.LastIndexOf('-');

            if ((tagIndex != -1) && (tagIndex + 1 < objectHandleValue.Length))
            {
                objectHandle.Tag = objectHandleValue[(tagIndex + 1)..];
            }

            return true;
        }
    }
}

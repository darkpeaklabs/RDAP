using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using DarkPeakLabs.Rdap.Values;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json converter for RDAP contact
    /// jCard JSON is defined in
    /// <see cref="https://tools.ietf.org/html/rfc7483">RFC 7483</see>
    /// and
    /// <see cref="https://tools.ietf.org/html/rfc6350">RFC 6350</see>
    /// </summary>
    internal class RdapContactConverter
    {
        public static bool TryGetValue(JsonNode jsonNode, RdapSerializerContext context, out RdapContact contact)
        {
            //Expected
            //["vcard", [
            //  /* VCard properties */
            //  ]
            //]

            contact = default;
            JsonValueKind valueKind = jsonNode.GetValueKind();
            switch (valueKind)
            {
                case JsonValueKind.Array:
                    var jsonArray = jsonNode.AsArray();
                    if (jsonArray.Count != 2)
                    {
                        context.AddJsonViolationError(jsonArray, "Unexpected number of items of a vCard array");
                        return false;
                    }

                    if (jsonArray[0] is not JsonValue vcardValue ||
                        !vcardValue.TryGetValue<string>(out var vcard) ||
                        !vcard.Equals("vcard", StringComparison.Ordinal))
                    {
                        context.AddJsonViolationError(jsonArray, "The first item in a vCard array must be a string with the value of 'vcard'");
                        return false;
                    }

                    if (jsonArray[1] is not JsonArray vcardPropertyJsonArray)
                    {
                        context.AddJsonViolationError(jsonArray, "The second item in a vCard array must be a an array of vCard values");
                        return false;
                    }

                    if (vcardPropertyJsonArray.Count == 0)
                    {
                        context.AddJsonViolationError(jsonArray, "vCard has not properties");
                        return false;
                    }

                    return TryGetValue(vcardPropertyJsonArray, context, out contact);

                default:
                    contact = default;
                    context.AddJsonViolationError(jsonNode, $"{valueKind} is not a valid JSON token type for vCard property {jsonNode.GetPropertyName()}");
                    return false;
            }
        }

        private static bool TryGetValue(JsonArray jsonArray, RdapSerializerContext context, out RdapContact contact)
        {
            contact = new();

            // Read VCard properties as array of arrays. Sample:
            // ["vcard",
            //  [
            //    ["version", {}, "text", "4.0"],
            //    ["fn", {}, "text", "John Doe"],
            //    ["gender", {}, "text", "M"],
            //    ["categories", {}, "text", "computers", "cameras"],
            //    ...
            //  ]
            //]

            for (int i = 0; i < jsonArray.Count; i++)
            {
                var propertyJsonNode = jsonArray[i];
                if (jsonArray[i] is not JsonArray vcardPropertyJsonArray)
                {
                    context.AddJsonViolationError(propertyJsonNode, "vCard property must be an array");
                    continue;
                }

                if (!TryGetVCardProperty(vcardPropertyJsonArray, context, out var property))
                {
                    continue;
                }

                switch (property.Name)
                {
                    case "version":
                        // ["version", {}, "text", "4.0" ]
                        // skip version property
                        ValidatePropertyType(property, "text", propertyJsonNode, context);
                        break;

                    case "fn":
                        if (ValidatePropertyType(property, "text", propertyJsonNode, context) &&
                            ValidateSingleValue(property, propertyJsonNode, context))
                        {
                            contact.FullName = property.Values[0];
                        }
                        break;

                    case "kind":
                        if (ValidatePropertyType(property, "text", propertyJsonNode, context) &&
                            ValidateSingleValue(property, propertyJsonNode, context))
                        {
                            if (property.Values[0].Equals("org", StringComparison.OrdinalIgnoreCase))
                            {
                                contact.Kind = RdapContactKind.Organization;
                            }
                            else if (property.Values[0].StartsWith("org", StringComparison.OrdinalIgnoreCase))
                            {
                                context.AddJsonViolationWarning(propertyJsonNode, $"{property.Values[0]} is not valid kind property value for organization.");
                                contact.Kind = RdapContactKind.Organization;
                            }
                            else if (RdapEnumConverter.TryParseString(property.Values[0], out RdapContactKind kind))
                            {
                                contact.Kind = kind;
                            }
                            else
                            {
                                context.AddJsonViolationError(propertyJsonNode, $"{property.Values[0]} is not valid kind property value.");
                                contact.Kind= RdapContactKind.Unknown;
                            }
                        }
                        break;

                    case "n":
                        if (ValidatePropertyType(property, "text", propertyJsonNode, context) &&
                            ValidateMultipleValues(property, propertyJsonNode, context))
                        {
                            contact.Names = property.Values;
                        }
                        break;

                    case "nickname":
                        if (ValidatePropertyType(property, "text", propertyJsonNode, context) &&
                            ValidateMultipleValues(property, propertyJsonNode, context))
                        {
                            contact.Nicknames = property.Values;
                        }
                        break;

                    case "adr":
                        if (ValidatePropertyType(property, "text", propertyJsonNode, context) &&
                            ValidateMultipleValues(property, propertyJsonNode, context))
                        {
                            contact.Address = property.Values;
                        }
                        break;

                    case "tel":
                        if (ValidatePropertyType(property, "uri", propertyJsonNode, context) &&
                            ValidateSingleValue(property, propertyJsonNode, context))
                        {
                            RdapContactPhoneNumber phoneNumber = new()
                            {
                                Value = property.Values[0]
                            };

                            // remove protocol part if present
                            if (phoneNumber.Value.StartsWith("tel:", StringComparison.OrdinalIgnoreCase))
                            {
                                phoneNumber.Value = phoneNumber.Value[4..];
                            }

                            // Create a list of phone number types
                            if (property.Parameters.TryGetValue("type", out var parameterValues))
                            {
                                List<RdapPhoneNumberType> phoneNumberTypes = [];
                                foreach (string parameterValue in parameterValues)
                                {
                                    if (RdapEnumConverter.TryParseString(parameterValue, out RdapPhoneNumberType phoneNumberType))
                                    {
                                        phoneNumberTypes.Add(phoneNumberType);
                                    }
                                    else
                                    {
                                        context.AddJsonViolationError(propertyJsonNode, $"\"{parameterValue}\" is not valid phone number type.");
                                        phoneNumberTypes.Add(RdapPhoneNumberType.Unknown);
                                    }
                                }
                                phoneNumber.Types = phoneNumberTypes;
                            }

                            List<RdapContactPhoneNumber> phoneNumbers = contact.PhoneNumbers == null ? [] : new(contact.PhoneNumbers);
                            phoneNumbers.Add(phoneNumber);
                            contact.PhoneNumbers = phoneNumbers;
                        }
                        break;

                    case "email":
                        if (ValidatePropertyType(property, "text", propertyJsonNode, context) &&
                            ValidateSingleValue(property, propertyJsonNode, context))
                        {
                            List<string> emails = contact.Emails == null ? [] : new(contact.Emails);
                            emails.Add(property.Values[0]);
                            contact.Emails = emails;
                        }
                        break;
                    // not implemented vCard properties
                    // general properties
                    case "source":
                    case "xml":
                    // identification properties
                    case "photo":
                    case "bday":
                    case "anniversary":
                    case "gender":
                    // delivery addressing properties
                    case "impp":
                    case "lang":
                    // geographic properties
                    case "tz":
                    case "geo":
                    // organizational properties
                    case "title":
                    case "role":
                    case "org":
                    case "logo":
                    case "member":
                    case "related":
                    // explanatory properties
                    case "categories":
                    case "note":
                    case "prodid":
                    case "rev":
                    case "sound":
                    case "uid":
                    case "clientpidmap":
                    case "url":
                    // security properties
                    case "key":
                    // calendar properties
                    case "fburl":
                    case "caladruri":
                    case "caluri":
                        break;

                    default:
                        context.AddJsonViolationError(propertyJsonNode, $"Unrecognized VCard property name {property.Name}");
                        break;
                }
            }

            return true;
        }

        private static bool ValidatePropertyType(VCardProperty property, string expectedType, JsonNode jsonNode, RdapSerializerContext context)
        {
            if (property.Type.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            context.AddJsonViolationError(jsonNode, $"vCard property {property.Name} has invalid data type {property.Type}. Expected type is {expectedType}.");
            return false;
        }

        private static bool ValidateSingleValue(VCardProperty property, JsonNode jsonNode, RdapSerializerContext context)
        {
            if (property.Values != null && property.Values.Count == 1)
            {
                return true;
            }
            context.AddJsonViolationError(jsonNode, $"vCard property {property.Name} should have a single value.");
            return false;
        }

        private static bool ValidateMultipleValues(VCardProperty property, JsonNode jsonNode, RdapSerializerContext context)
        {
            if (property.Values != null && property.Values.Count >= 1)
            {
                return true;
            }
            context.AddJsonViolationError(jsonNode, $"vCard property {property.Name} should have one or more values.");
            return false;
        }

        private static bool TryGetVCardProperty(JsonArray jsonArray, RdapSerializerContext context, out VCardProperty property)
        {
            property = new();

            if (jsonArray.Count < 4)
            {
                context.AddJsonViolationError(jsonArray, "vCard property array must contain at least 4 items");
                return false;
            }

            if (jsonArray[0] is not JsonValue nameJsonValue ||
                !nameJsonValue.TryGetValue(out property.Name))
            {
                context.AddJsonViolationError(jsonArray, "The first item in a vCard array must be a string with the value of 'vcard'");
                return false;
            }

            if (property.Name.Any(x => char.IsUpper(x)))
            {
                context.AddJsonViolationError(jsonArray, $"VCard property item MUST be lowercase string");
                property.Name = property.Name.ToLowerInvariant();
            }

            if (jsonArray[1] is not JsonObject parametersJsonObject)
            {
                context.AddJsonViolationError(jsonArray, "The second item in a vCard array must be an object holding property parameters");
                return false;
            }

            if (!TryGetVCardPropertyParameters(parametersJsonObject, context, out property.Parameters))
            {
                return false;
            }

            if (jsonArray[2] is not JsonValue typeJsonObject ||
                !typeJsonObject.TryGetValue(out property.Type))
            {
                context.AddJsonViolationError(jsonArray, "The third item in a vCard array must be a data type string");
                return false;
            }

            if (!TryGetStringValues(jsonArray[3], out property.Values))
            {
                context.AddJsonViolationError(jsonArray, $"vCard property {property.Name} value is not valid");
                return false;
            }

            return true;
        }

        private static bool TryGetVCardPropertyParameters(JsonObject jsonObject, RdapSerializerContext context, out Dictionary<string, List<string>> propertyParameters)
        {
            propertyParameters = [];

            foreach((string name, JsonNode node) in jsonObject)
            {
                string key = name;
                if (name.Any(x => char.IsUpper(x)))
                {
                    context.AddJsonViolationWarning(jsonObject, "VCard property parameter name MUST be a lowercase string");
                    key = key.ToLowerInvariant();
                }

                if (!TryGetStringValues(node, out var values))
                {
                    context.AddJsonViolationError(node, "Invalid VCard property parameter value");
                    return false;
                }

                if (!propertyParameters.TryAdd(key, values))
                {
                    context.AddJsonViolationError(node, $"Duplicate VCard property parameter {name}");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetStringValues(JsonNode node, out List<string> values)
        {
            values = [];

            switch (node)
            {
                case JsonValue jsonValue:
                    if (!jsonValue.TryGetValue<string>(out var value))
                    {
                        return false;
                    }
                    values.Add(value);
                    return true;

                case JsonArray jsonArray:
                    foreach (var itemJsonNode in jsonArray)
                    {
                        if (!TryGetStringValues(itemJsonNode, out var itemValue))
                        {
                            return false;
                        }
                        values.AddRange(itemValue);
                    }
                    return true;

                default:
                    return false;
            }
        }
    }
}

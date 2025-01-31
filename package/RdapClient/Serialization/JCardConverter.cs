using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using DarkPeakLabs.Rdap.Conformance;
using DarkPeakLabs.Rdap.Values;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json converter for jCard JSON defined in
    /// <see cref="https://tools.ietf.org/html/rfc7483">RFC 7483</see>
    /// and
    /// <see cref="https://tools.ietf.org/html/rfc6350">RFC 6350</see>
    /// </summary>
    internal class JCardConverter : JsonConverter<RdapContact>
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
        /// Starting JSON data depth
        /// </summary>
        private int jCardStartDepth;

        /// <summary>
        /// Starting token type
        /// </summary>
        private JsonTokenType jCardStartTokenType;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="errors"></param>
        public JCardConverter(RdapConformance conformance, ILogger logger = null)
        {
            this.conformance = conformance;
            _logger = logger;
        }

        #region Conformance

        /// <summary>
        /// Helper function for error logging
        /// </summary>
        /// <param name="text"></param>
        /// <param name="reader">JSON reader</param>
        private void AddConformanceViolationError(string message, ref Utf8JsonReader reader)
        {
            conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, message);
        }

        /// <summary>
        /// Helper function for warning logging
        /// </summary>
        /// <param name="text"></param>
        /// <param name="reader">JSON reader</param>
        private void AddConformanceViolationWarning(string message, ref Utf8JsonReader reader)
        {
            conformance.AddJsonViolation(RdapConformanceViolationSeverity.Error, ref reader, message);
        }

        #endregion Conformance

        #region JSON Read Utility Methods

        /// <summary>
        /// Function tries to read value of jCard single-value property
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool TryReadSingleStringValue(ref Utf8JsonReader reader, out string result)
        {
            using (var _scope = _logger?.BeginScope("TryReadSingleStringValue"))
            {
                _logger?.LogDebug("Reading single string property value");
                // read the next token which is expected to be string
                ReadToken(ref reader);
                if (reader.TokenType == JsonTokenType.String)
                {
                    result = reader.GetString();
                    _logger?.LogDebug("Read string value {Value}", result);
                    if (string.IsNullOrEmpty(result))
                    {
                        AddConformanceViolationWarning($"Property value is empty string.", ref reader);
                    }
                }
                else
                {
                    result = null;
                    AddConformanceViolationError($"String value expected.", ref reader);
                    return false;
                }
                // next token after single value property must be end array
                ReadToken(ref reader);
                if (reader.TokenType != JsonTokenType.EndArray)
                {
                    AddConformanceViolationError($"Next token after single value property must be end array.", ref reader);
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Function tries to read array of strings until EndArray is encountered
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="values"></param>
        /// <returns></returns>
        private bool TryReadStringsUntilArrayEnd(ref Utf8JsonReader reader, out List<string> values)
        {
            using (var _scope = _logger?.BeginScope("TryReadStringsUntilArrayEnd"))
            {
                _logger?.LogDebug("Reading strings until end of array token");

                // read JSON token
                ReadToken(ref reader);
                values = new List<string>();

                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    if (reader.TokenType != JsonTokenType.String)
                    {
                        AddConformanceViolationError($"String value expected.", ref reader);
                        return false;
                    }
                    string value = reader.GetString();
                    _logger?.LogDebug("Adding string value {Value}", value);
                    values.Add(value);
                    ReadToken(ref reader);
                }

                return true;
            }
        }

        /// <summary>
        /// Function tries to read jCard structured-value property value as list of strings
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="result"></param>
        private bool TryReadStructuredValue(ref Utf8JsonReader reader, out List<string> result)
        {
            using (var _scope = _logger?.BeginScope("TryReadStructuredValue"))
            {
                _logger?.LogDebug("Reading structured property value");

                // read next token and verifies it is start array
                ReadToken(ref reader);
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    result = null;
                    AddConformanceViolationError($"JCard structured value must starts with StartArray token.", ref reader);
                    return false;
                }

                ReadToken(ref reader);
                result = new List<string>();

                // structured value can consists of strings or arrays of strings
                while (reader.TokenType != JsonTokenType.EndArray)
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.String:
                            string value = reader.GetString();
                            _logger?.LogDebug("Adding string value {Value}", value);
                            result.Add(value);
                            break;

                        case JsonTokenType.StartArray:
                            _logger?.LogDebug("Reading array of strings");
                            if (TryReadStringsUntilArrayEnd(ref reader, out List<string> values))
                            {
                                result.AddRange(values);
                            }
                            else
                            {
                                AddConformanceViolationError($"Error reading string array section of structured property value.", ref reader);
                                return false;
                            }
                            break;

                        default:
                            _logger?.LogError("Unexpected token type {TokenType}", reader.TokenType);
                            AddConformanceViolationError($"Unexpected JSON token.", ref reader);
                            return false;
                    }
                    ReadToken(ref reader);
                }
                // read the array end token at the end of the structured value
                ReadToken(ref reader);

                if (result.Count == 0)
                {
                    AddConformanceViolationWarning($"Property structured value has no items.", ref reader);
                }
                if (result.Any(v => !string.IsNullOrEmpty(v)))
                {
                    AddConformanceViolationWarning($"Property structured value items are all empty strings.", ref reader);
                }

                return true;
            }
        }

        /// <summary>
        /// Function tries to read jCard multi-value property value
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="result">List of values</param>
        /// <returns></returns>
        private bool TryReadMultiStringValue(ref Utf8JsonReader reader, out List<string> result)
        {
            using (var _scope = _logger?.BeginScope("TryReadMultiStringValue"))
            {
                _logger?.LogDebug("Reading multi-value property value");

                result = null;
                if (!TryReadStringsUntilArrayEnd(ref reader, out List<string> values))
                {
                    AddConformanceViolationError($"Error reading multi-value property value.", ref reader);
                    return false;
                }
                result = values;

                if (result.Count == 0)
                {
                    AddConformanceViolationWarning($"Multi-value property has no items.", ref reader);
                }
                if (result.Any(v => !string.IsNullOrEmpty(v)))
                {
                    AddConformanceViolationWarning($"Multi-value property items are all empty strings.", ref reader);
                }
                return true;
            }
        }

        /// <summary>
        /// Function parses string to contact kind enum
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>Contact kind value</returns>
        private RdapContactKind ParseContactKind(string value, ref Utf8JsonReader reader)
        {
            _logger?.LogDebug("Parsing string value {Value} to ContactKind", value);

            // the kind can be org or organization (invalid)
            if (value.Equals("org", StringComparison.OrdinalIgnoreCase))
            {
                return RdapContactKind.Organization;
            }
            else if (value.StartsWith("org", StringComparison.OrdinalIgnoreCase))
            {
                AddConformanceViolationWarning($"{value} is not valid kind property value for organization.", ref reader);
                return RdapContactKind.Organization;
            }
            else
            {
                // try parsing value
                if (RdapEnumHelper.TryParseString<RdapContactKind>(value, out RdapContactKind kind))
                {
                    return kind;
                }
                AddConformanceViolationError($"{value} is not valid kind property value.", ref reader);
                return RdapContactKind.Unknown;
            }
        }

        /// <summary>
        /// Function creates phone number object from string value
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="propertyParameters">JCard property parameters</param>
        /// <returns></returns>
        private RdapContactPhoneNumber GetPhoneNumber(string value, Dictionary<string, List<string>> propertyParameters, ref Utf8JsonReader reader)
        {
            _logger?.LogDebug("Parsing string value {Value} to phone number", value);

            // remove protocol part if present
            if (value.StartsWith("tel:", StringComparison.OrdinalIgnoreCase))
            {
                value = value[4..];
            }

            // Create a list of phone number types
            List<RdapPhoneNumberType> phoneNumberTypes = null;
            if (propertyParameters.TryGetValue("type", out var types))
            {
                phoneNumberTypes = [];
                foreach (string type in types)
                {
                    if (RdapEnumHelper.TryParseString(type, out RdapPhoneNumberType result))
                    {
                        phoneNumberTypes.Add(result);
                    }
                    else
                    {
                        AddConformanceViolationError($"\"{type}\" is not valid phone number type.", ref reader);
                        phoneNumberTypes.Add(RdapPhoneNumberType.Unknown);
                    }
                }
            }

            return new RdapContactPhoneNumber()
            {
                Value = value,
                Types = phoneNumberTypes
            };
        }

        /// <summary>
        /// Method verifies token type and skips all JCard data if verification fails
        /// Method logs the error
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="expectedTokenType"></param>
        /// <returns></returns>
        private bool VerifyTokenTypeOrSkipJCard(ref Utf8JsonReader reader, JsonTokenType expectedTokenType)
        {
            _logger?.LogDebug("Verifying current token type. Current = {CurrentTokenType}, Expected = {ExpectedTokenType}", reader.TokenType, expectedTokenType);
            if (reader.TokenType != expectedTokenType)
            {
                _logger?.LogError("Encountered unexpected token type {TokenType}. Expected = {ExpectedTokenType}", reader.TokenType, expectedTokenType);
                AddConformanceViolationError($"Expected JSON token {expectedTokenType}.", ref reader);
                SkipJCard(ref reader);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Function reads next Json token
        /// </summary>
        /// <param name="reader">JSON reader</param>
        private void ReadToken(ref Utf8JsonReader reader)
        {
            if (!reader.Read())
            {
                _logger?.LogCritical("Error reading JSON data.");
                throw new RdapJsonException("Error reading JSON data.", ref reader);
            }
            _logger?.LogDebug("Read JSON token type {TokenType} at depth {Depth}", reader.TokenType, reader.CurrentDepth);
        }

        /// <summary>
        /// Reads JSON data until specific token type at specific depth
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="tokenType">Token type</param>
        /// <param name="depth">Token depth</param>
        private void ReadUntil(ref Utf8JsonReader reader, JsonTokenType tokenType, int depth)
        {
            _logger?.LogDebug("Read JSON data until token {TokenType} at depth {Depth}", tokenType, depth);
            while (reader.TokenType != tokenType || reader.CurrentDepth > depth)
            {
                ReadToken(ref reader);
            };
        }

        /// <summary>
        /// Method tries to read string property item from property array
        /// Used to read property name and type
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="result"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "RFC requires lowercase")]
        private bool TryReadLowerCasePropertyItem(ref Utf8JsonReader reader, out string result)
        {
            _logger?.LogDebug("Reading JCard property item");
            result = null;
            //read property item
            ReadToken(ref reader);
            if (reader.TokenType != JsonTokenType.String)
            {
                _logger?.LogError("Expected string token. Current token type = {TokenType}", reader.TokenType);
                AddConformanceViolationError($"Expected string VCard property item", ref reader);
                return false;
            }
            result = reader.GetString();
            _logger?.LogDebug("Read string value {Value}", result);
            if (string.IsNullOrEmpty(result))
            {
                _logger?.LogError("Unexpected empty string");
                AddConformanceViolationError($"VCard property item CANNOT be empty string", ref reader);
                return false;
            }
            if (result.Any(x => Char.IsUpper(x)))
            {
                AddConformanceViolationWarning($"VCard property item MUST be lowercase string", ref reader);
                result = result.ToLowerInvariant();
            }
            return true;
        }

        /// <summary>
        /// Methos tries to read property parameters object
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="propertyParameters"></param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "RFC requires lowercase")]
        private bool TryReadPropertyParameters(ref Utf8JsonReader reader, out Dictionary<string, List<string>> propertyParameters)
        {
            using (var _scope = _logger?.BeginScope("TryReadPropertyParameters"))
            {
                _logger?.LogDebug("Reading JCard property parameters");

                // create parameters dictionary
                propertyParameters = new Dictionary<string, List<string>>();

                // Next token must be start object
                ReadToken(ref reader);
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    _logger?.LogError("Unexpected token type {TokenType}. Expected = {ExpectedTokenType}", reader.TokenType, JsonTokenType.StartObject);
                    AddConformanceViolationError($"Second item of VCard property MUST be an object.", ref reader);
                    return false;
                }

                // read properties until object end token
                // property value can be a string or array of strings
                ReadToken(ref reader);
                while (reader.TokenType != JsonTokenType.EndObject)
                {
                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        _logger?.LogError("Unexpected token type {TokenType}. Expected = {ExpectedTokenType}", reader.TokenType, JsonTokenType.PropertyName);
                        AddConformanceViolationError($"Expected property name.", ref reader);
                        return false;
                    }

                    // get the property name
                    string key = reader.GetString();
                    _logger?.LogDebug("Read property parameter name {ParameterName}", key);
                    if (key.Any(x => Char.IsUpper(x)))
                    {
                        AddConformanceViolationWarning($"VCard property parameter name MUST be lowercase string.", ref reader);
                        key = key.ToLowerInvariant();
                    }

                    ReadToken(ref reader);
                    List<string> parameterValues = new List<string>();

                    // read property value as list of string
                    _logger?.LogDebug("Reading property parameter values as list of strings");
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.String:
                            var value = reader.GetString();
                            _logger?.LogDebug("Adding string value {Value}", value);
                            parameterValues.Add(value);
                            break;

                        case JsonTokenType.StartArray:
                            _logger?.LogDebug("Reading array of string values");
                            if (TryReadStringsUntilArrayEnd(ref reader, out List<string> values))
                            {
                                parameterValues.AddRange(values);
                            }
                            else
                            {
                                AddConformanceViolationError($"Error reading property parameter {key} values as array.", ref reader);
                                return false;
                            }
                            break;

                        default:
                            _logger?.LogError("Unexpected token type {TokenType}. Expected string or start of array", reader.TokenType);
                            AddConformanceViolationError($"Unexpected token type reading property parameter \"{key}\".", ref reader);
                            return false;
                    }

                    // check if property name is duplicate
                    if (!propertyParameters.TryAdd(key, parameterValues))
                    {
                        AddConformanceViolationWarning($"Duplicate property parameter \"{key}\".", ref reader);
                    }
                    ReadToken(ref reader);
                }
                return true;
            }
        }

        /// <summary>
        /// Function advances reader to the end of JCard data
        /// </summary>
        /// <param name="reader">JSON reader</param>
        private void SkipJCard(ref Utf8JsonReader reader)
        {
            _logger?.LogDebug("Advancing JSON reader to the end of JCard data");
            switch (jCardStartTokenType)
            {
                case JsonTokenType.StartArray:
                    ReadUntil(ref reader, JsonTokenType.EndArray, jCardStartDepth);
                    break;

                case JsonTokenType.StartObject:
                    ReadUntil(ref reader, JsonTokenType.EndObject, jCardStartDepth);
                    break;

                default:
                    //single value - advancing reader is not necessary
                    break;
            }
        }

        /// <summary>
        /// Method verifies property type
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <param name="propertyType">Property type</param>
        /// <param name="expectedType">Expected property type</param>
        /// <returns></returns>
        private bool VerifyPropertyType(string propertyName, string propertyType, string expectedType, ref Utf8JsonReader reader)
        {
            _logger?.LogDebug("Verifying type of property {PropertyName}. Current = {PropertyType}, Expected = {ExpectedPropertyType}", propertyName, propertyType, expectedType);
            if (propertyType.Equals(expectedType, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            _logger?.LogError("Property {PropertyName} has unexpected type {PropertyType}. Expected = {ExpectedPropertyType}", propertyName, propertyType, expectedType);
            AddConformanceViolationError($"Property {propertyName} has unexpected data type {propertyType}. Expected type {expectedType}.", ref reader);
            return false;
        }

        #endregion JSON Read Utility Methods

        #region JCard JsonConverter

        /// <summary>
        /// Reads jCard JSON data
        /// </summary>
        /// <param name="reader">JSON reader</param>
        /// <param name="typeToConvert">Data type to convert JSON data to</param>
        /// <param name="options">Serializer options</param>
        /// <returns></returns>
        public override RdapContact Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            _logger?.LogDebug("Reading JCard JSON data");

            //store current depth
            jCardStartDepth = reader.CurrentDepth;
            jCardStartTokenType = reader.TokenType;

            //Expected
            //["vcard", [
            //  /* VCard properties */
            //  ]
            //]
            if (!VerifyTokenTypeOrSkipJCard(ref reader, JsonTokenType.StartArray))
            {
                _logger?.LogError("JCard JSON data does not start with start of array token.");
                return null;
            }

            // read next token and verify type and value
            ReadToken(ref reader);
            if (!VerifyTokenTypeOrSkipJCard(ref reader, JsonTokenType.String))
            {
                _logger?.LogError("First JCard JSON data array item is not string.");
                return null;
            }
            if (!reader.ValueTextEquals("vcard"))
            {
                _logger?.LogError("First JCard JSON data array item is not equal \"vcard\".");
                AddConformanceViolationError($"Expected string \"vcard\"", ref reader);
                SkipJCard(ref reader);
                return null;
            }

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

            // read next token and verify it is start array
            ReadToken(ref reader);
            if (!VerifyTokenTypeOrSkipJCard(ref reader, JsonTokenType.StartArray))
            {
                _logger?.LogError("Second JCard JSON data array item is not an array.");
                return null;
            }

            //create empty contact object
            RdapContact contact = new RdapContact()
            {
                Kind = RdapContactKind.Unknown
            };

            // capture depth at the start of properties array
            int propertiesArrayStartDepth = reader.CurrentDepth;

            // check if any properties are present
            ReadToken(ref reader);
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                AddConformanceViolationWarning("JCard has no properties", ref reader);
                SkipJCard(ref reader);
                return contact;
            }

            // loop through array of properties
            // each property is an array
            _logger?.LogDebug("Reading JCard properties");
            while (reader.CurrentDepth > propertiesArrayStartDepth)
            {
                // read start of property array
                if (reader.TokenType != JsonTokenType.StartArray)
                {
                    _logger?.LogWarning("Skipping token with unexpected token type {TokenType}.", reader.TokenType);
                    reader.Skip();
                    ReadToken(ref reader);
                    continue;
                }

                // capture depth at property start
                int propertyStartDepth = reader.CurrentDepth;

                // read property name
                if (!TryReadLowerCasePropertyItem(ref reader, out string propertyName))
                {
                    _logger?.LogWarning("Unable to read property name - skipping property.");
                    // skip property
                    ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                    ReadToken(ref reader);
                    continue;
                }
                _logger?.LogDebug("Property name {PropertyName}", propertyName);

                using (IDisposable _scope = _logger?.BeginScope($"JCard property {propertyName}"))
                {
                    // read property parameters
                    if (!TryReadPropertyParameters(ref reader, out Dictionary<string, List<string>> propertyParameters))
                    {
                        _logger?.LogWarning("Unable to read property parameters - skipping property.");
                        // skip property
                        ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                        ReadToken(ref reader);
                        continue;
                    }

                    // read property type
                    if (!TryReadLowerCasePropertyItem(ref reader, out string propertyType))
                    {
                        _logger?.LogWarning("Unable to read property type - skipping property.");
                        // skip property
                        ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                        ReadToken(ref reader);
                        continue;
                    }
                    _logger?.LogDebug("Property type {PropertyType}", propertyType);

                    // read property value
                    switch (propertyName)
                    {
                        case "version":
                            // ["version", {}, "text", "4.0" ]
                            // skip version property
                            ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            break;

                        case "fn":
                            if (VerifyPropertyType(propertyName, propertyType, "text", ref reader) && TryReadSingleStringValue(ref reader, out string fn))
                            {
                                contact.FullName = fn;
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            }
                            break;

                        case "kind":
                            if (VerifyPropertyType(propertyName, propertyType, "text", ref reader) && TryReadSingleStringValue(ref reader, out string kind))
                            {
                                contact.Kind = ParseContactKind(kind, ref reader);
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            }
                            break;

                        case "n":
                            if (VerifyPropertyType(propertyName, propertyType, "text", ref reader) && TryReadStructuredValue(ref reader, out List<string> names))
                            {
                                contact.Names = names;
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            }
                            break;

                        case "nickname":
                            if (VerifyPropertyType(propertyName, propertyType, "text", ref reader) && TryReadMultiStringValue(ref reader, out List<string> nicknames))
                            {
                                contact.Nicknames = nicknames;
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            }
                            break;

                        case "adr":
                            if (VerifyPropertyType(propertyName, propertyType, "text", ref reader) && TryReadStructuredValue(ref reader, out List<string> adr))
                            {
                                contact.Address = adr;
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            }
                            break;

                        case "tel":
                            if (contact.PhoneNumbers == null)
                            {
                                contact.PhoneNumbers = new List<RdapContactPhoneNumber>();
                            }
                            if (VerifyPropertyType(propertyName, propertyType, "uri", ref reader) && TryReadSingleStringValue(ref reader, out string tel))
                            {
                                (contact.PhoneNumbers as List<RdapContactPhoneNumber>).Add(GetPhoneNumber(tel, propertyParameters, ref reader));
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            }
                            break;

                        case "email":
                            if (contact.Emails == null)
                            {
                                contact.Emails = new List<string>();
                            }
                            if (VerifyPropertyType(propertyName, propertyType, "text", ref reader) && TryReadSingleStringValue(ref reader, out string email))
                            {
                                (contact.Emails as List<string>).Add(email);
                            }
                            else
                            {
                                ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
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
                            ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            break;

                        default:
                            _logger?.LogWarning("Unrecognized VCard property name {PropertyName}", propertyName);
                            AddConformanceViolationError($"Unrecognized VCard property name {propertyName}", ref reader);
                            ReadUntil(ref reader, JsonTokenType.EndArray, propertyStartDepth);
                            break;
                    }
                }

                _logger?.LogDebug("Finished reading property {PropertyName}", propertyName);
                ReadToken(ref reader);
            }

            // all properties read
            // read closing end array
            _logger?.LogDebug("Finished reading all JCard properties");
            ReadToken(ref reader);
            if (reader.TokenType != JsonTokenType.EndArray)
            {
                _logger?.LogError("Unexpected token type {TokenType}. Expected end of array", reader.TokenType);
                AddConformanceViolationWarning("Expecting end of array after array of properties", ref reader);
                SkipJCard(ref reader);
            }

            return contact;
        }

        /// <summary>
        /// Writes jCard JSON data
        /// </summary>
        /// <param name="writer">JSON writer</param>
        /// <param name="value">Instance of RdapContact class to write JSON data for</param>
        /// <param name="options">Serializer options</param>
        public override void Write(Utf8JsonWriter writer, RdapContact value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        #endregion JCard JsonConverter
    }
}

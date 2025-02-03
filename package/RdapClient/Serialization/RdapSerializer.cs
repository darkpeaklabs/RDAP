using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// RDAP JSON serializer
    /// </summary>
    internal class RdapSerializer
    {
        private static readonly JsonSerializerOptions _deserializeOptions = new();

        public static bool HandleUndefinedEnumValue { get; set; }

        /// <summary>
        /// Function deserializes Json to RDAP response object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="conformance"></param>
        /// <returns></returns>
        internal static T Deserialize<T>(string json, RdapConformance conformance = null, ILogger logger = null)
            where T : class
        {
            RdapSerializerContext context = new(
                conformance ?? new RdapConformance(),
                logger);
            
            var node = JsonSerializer.Deserialize<JsonNode>(json, _deserializeOptions);
            return CreateRdapObject<T>(node, context);
        }

        private static T CreateRdapObject<T>(JsonNode node, RdapSerializerContext context)
            where T : class
        {
            T instance = Activator.CreateInstance<T>();
            var type = instance.GetType();
            CreateRdapObject(instance, type, node, context);
            return instance;
        }

        private static object CreateRdapObject(Type type, JsonNode node, RdapSerializerContext context)
        {
            if (type == typeof(RdapObjectHandle))
            {
                if (RdapObjectHandleConverter.TryGetValue(node, context, out var value))
                {
                    return value;
                }
                return null;
            }
            else if (type == typeof(RdapContact))
            {
                if (RdapContactConverter.TryGetValue(node, context, out var value))
                {
                    return value;
                }
                return null;
            }

            var instance = Activator.CreateInstance(type);
            return CreateRdapObject(instance, type, node, context);
        }

        private static object CreateRdapObject(object instance, Type type, JsonNode node, RdapSerializerContext context)
        {
            if (node is not JsonObject)
            {
                context.AddJsonViolationError(node, $"JSON token does not represent an object");
                return null;
            }

            foreach(var property in type.GetJsonProperties())
            {
                var propertyNode = node[property.JsonPropertyName];
                if (propertyNode == null)
                {
                    if (property.IsRequired)
                    {
                        context.AddJsonViolationError(node, $"Required JSON property {property.JsonPropertyName} is not found");
                    }
                    continue;
                }

                context.LogTrace("Serializing property {PropertyName}", property.JsonPropertyName);

                if (property.IsJsonPrimitive)
                {
                    SetPropertyValue(
                        property,
                        instance,
                        value: GetJsonPrimitiveValue(
                            property.IsNullable ? property.NullableUnderlyingType : property.PropertyInfo.PropertyType,
                            propertyNode,
                            property.JsonPropertyName,
                            context));
                    continue;
                }
                else if (property.PropertyInfo.PropertyType.IsClass)
                {
                    SetPropertyValue(
                        property,
                        instance,
                        value: CreateRdapObject(
                            property.PropertyInfo.PropertyType,
                            propertyNode,
                            context));
                    continue;
                }
                else if (property.IsEnumerable)
                {
                    if (propertyNode is not JsonArray)
                    {
                        context.AddJsonViolationError(propertyNode, $"JSON property {property.JsonPropertyName} is expected to be an array");
                        continue;
                    }

                    var jsonArray = propertyNode.AsArray();
                    IList list = null;

                    foreach(var itemNode in jsonArray)
                    {
                        object itemValue;
                        if (property.IsEnumerableItemTypeJsonPrimitive)
                        {
                            itemValue = GetJsonPrimitiveValue(
                                property.EnumerableItemType,
                                itemNode,
                                property.JsonPropertyName,
                                context);
                        }
                        else
                        {
                            itemValue = CreateRdapObject(
                                property.EnumerableItemType,
                                itemNode,
                                context);
                        }

                        if (itemValue != null)
                        {
                            list ??= (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(property.EnumerableItemType));
                            list.Add(itemValue);
                        }
                    }

                    SetPropertyValue(property, instance, value: list);
                    continue;
                }
            }
            return instance;
        }

        private static void SetPropertyValue(RdapJsonProperty property, object instance, object value)
        {
            if (value == null)
            {
                return;
            }
            property.PropertyInfo.SetValue(instance, value);
        }

        private static object GetJsonPrimitiveValue(Type type, JsonNode node, string propertyName, RdapSerializerContext context)
        {
            if (node is not JsonValue jsonValue)
            {
                context.AddJsonViolationError(node, $"Property {propertyName} is expected to be a primitive JSON value");
                return null;
            }

            if (type.IsEnum)
            {
                var typeAttributes = type.GetCustomAttributes(inherit: true);
                if (typeAttributes.Any(x => x.GetType() == typeof(FlagsAttribute)))
                {
                    if (RdapFlagsEnumConverter.TryGetValue(jsonValue, type, context, out var value))
                    {
                        return value;
                    };
                }
                else if (RdapEnumConverter.TryGetValue(jsonValue, type, context, out var value))
                {
                    return value;
                };
                return null;
            }
            else if (type == typeof(string))
            {
                if (RdapStringConverter.TryGetValue(jsonValue, context, out var value))
                {
                    return value;
                };
            }
            else if (type == typeof(int))
            {
                if (RdapIntConverter.TryGetValue(jsonValue, context, out var value))
                {
                    return value;
                };
            }
            else if (type == typeof(bool))
            {
                if (RdapBooleanConverter.TryGetValue(jsonValue, context, out var value))
                {
                    return value;
                };
            }
            else if (type == typeof(DateTimeOffset))
            {
                if (RdapDateTimeConverter.TryGetValue(jsonValue, context, out var value))
                {
                    return value;
                };
            }
            else
            {
                throw new NotImplementedException();
            }

            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal class RdapJsonProperty
    {
        private static readonly Type enumerableType = typeof(IEnumerable<>);

        public RdapJsonProperty(PropertyInfo property)
        {
            PropertyInfo = property;

            var attributes = property.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                switch(attribute)
                {
                    case JsonPropertyNameAttribute JsonPropertyNameAttribute:
                        JsonPropertyName = JsonPropertyNameAttribute.Name;
                        IsJsonProperty = true;
                        break;
                    case RequiredAttribute:
                        IsRequired = true;
                        break;
                }
            }

            var interfaces = property.PropertyType.GetInterfaces();
            foreach(var interfaceType in interfaces)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == enumerableType)
                {
                    IsEnumerable = true;
                    var genericArguments = interfaceType.GetGenericArguments();
                    // TODO: validate in test
                    EnumerableItemType = genericArguments[0];
                    IsEnumerableItemTypeJsonPrimitive = IsTypeJsonPrimitive(EnumerableItemType);
                }
            }

            NullableUnderlyingType = Nullable.GetUnderlyingType(property.PropertyType);
            IsNullable = NullableUnderlyingType != null;

            IsJsonPrimitive = IsTypeJsonPrimitive(property.PropertyType)
                || (IsNullable && IsTypeJsonPrimitive(NullableUnderlyingType));
        }

        public PropertyInfo PropertyInfo { get; private set; }
        public string JsonPropertyName { get; }
        public bool IsJsonProperty { get; }
        public bool IsRequired { get; }
        public bool IsEnumerable { get; }
        public Type EnumerableItemType { get; }
        public bool IsEnumerableItemTypeJsonPrimitive { get; }
        public Type NullableUnderlyingType { get; }
        public bool IsNullable { get; }
        public bool IsJsonPrimitive { get; }

        private static bool IsTypeJsonPrimitive(Type type)
        {
            return type.IsPrimitive
                || type == typeof(string)
                || type == typeof(DateTimeOffset)
                || type.IsEnum;
        }
    }
}

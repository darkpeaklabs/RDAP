using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Json converter factory for RDAP enum types
    /// </summary>
    internal class RdapEnumConverterFactory : JsonConverterFactory
    {
        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger _logger;

        private readonly RdapConformance conformance;

        internal RdapEnumConverterFactory(RdapConformance conformance, ILogger logger = null)
        {
            _logger = logger;
            this.conformance = conformance;
        }

        /// <summary>
        /// Function determines whether it can handle given type
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <returns></returns>
        public override bool CanConvert(Type typeToConvert)
        {
            if (typeToConvert.IsEnum)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Function creates a JSON converter for given type
        /// </summary>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(RdapEnumConverter<>).MakeGenericType(
                    new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options, conformance, _logger },
                culture: null);

            return converter;
        }
    }
}
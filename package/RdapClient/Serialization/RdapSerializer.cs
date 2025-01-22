using DarkPeakLabs.Rdap.Conformance;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

namespace DarkPeakLabs.Rdap.Serialization
{
    /// <summary>
    /// Helper class for deserialization of RDAP JSON response
    /// </summary>
    internal class RdapSerializer
    {
        internal static bool ThrowOnError;

        /// <summary>
        /// Function deserializes Json to RDAP response object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        internal static T Deserialize<T>(string json, ILogger logger = null)
        {
            return Deserialize<T>(json, out RdapConformance _, logger);
        }

        /// <summary>
        /// Function deserializes Json to RDAP response object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="conformance"></param>
        /// <returns></returns>
        internal static T Deserialize<T>(string json, out RdapConformance conformance, ILogger logger = null)
        {
            conformance = new RdapConformance();

#pragma warning disable CA1869 // Cache and reuse 'JsonSerializerOptions' instances
            var deserializeOptions = new JsonSerializerOptions();
#pragma warning restore CA1869 // Cache and reuse 'JsonSerializerOptions' instances

            deserializeOptions.Converters.Add(new RdapEnumConverterFactory(conformance, logger));
            deserializeOptions.Converters.Add(new JCardConverter(conformance, logger));
            deserializeOptions.Converters.Add(new RdapObjectHandleConverter(conformance, logger));
            deserializeOptions.Converters.Add(new RdapDateTimeConverter(conformance, logger));
            deserializeOptions.Converters.Add(new RdapStringConverter(conformance, logger));
            deserializeOptions.Converters.Add(new RdapStringCollectionConverter(conformance, logger));
            deserializeOptions.Converters.Add(new RdapNullableIntConverter(conformance, logger));
            deserializeOptions.Converters.Add(new RdapNullableBooleanConverter(conformance, logger));

            try
            {
                return JsonSerializer.Deserialize<T>(json, deserializeOptions);
            }
            catch (RdapJsonException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new RdapJsonException(exception.Message, exception);
            }
        }
    }
}
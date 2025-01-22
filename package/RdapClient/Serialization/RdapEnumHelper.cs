using Microsoft.Extensions.Logging;
using System;

namespace DarkPeakLabs.Rdap.Serialization
{
    internal static class RdapEnumHelper
    {
        /// <summary>
        /// Attempt to parse string value to enum
        /// </summary>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        internal static bool TryParseString<T>(string value, out T result, ILogger logger = null) where T : struct
        {
            logger?.LogDebug("Trying to convert string value {Value} to type {EnumType}", value, typeof(T).Name);
            return Enum.TryParse<T>(value
                .Replace(" ", "", StringComparison.Ordinal)
                .Replace("-", "", StringComparison.Ordinal)
                .Replace(".", "", StringComparison.Ordinal)
                .Replace("_", "", StringComparison.Ordinal), true, out result);
        }
    }
}
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
        internal static bool TryParseString<T>(string value, out T result) where T : struct
        {
            return Enum.TryParse(
                value
                .Replace(" ", "", StringComparison.Ordinal)
                .Replace("-", "", StringComparison.Ordinal)
                .Replace(".", "", StringComparison.Ordinal)
                .Replace("_", "", StringComparison.Ordinal), true, out result);
        }

        internal static bool TryParseString(string value, Type enumType, out object result)
        {
            return Enum.TryParse(
                enumType,
                value.Replace(" ", "", StringComparison.Ordinal)
                    .Replace("-", "", StringComparison.Ordinal)
                    .Replace(".", "", StringComparison.Ordinal)
                    .Replace("_", "", StringComparison.Ordinal),
                ignoreCase: true,
                out result);
        }
    }
}

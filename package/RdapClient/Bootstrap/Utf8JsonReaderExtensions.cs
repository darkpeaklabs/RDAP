using System.Collections.Generic;
using System.Text.Json;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// UTF-8 JSON reader extensions
/// </summary>
internal static class Utf8JsonReaderExtensions
{
    /// <summary>
    /// Read an array of specified number of arrays of string
    /// </summary>
    /// <param name="reader">UTF-8 JSON reader reference</param>
    /// <param name="numberOfArrays">Expected number of arrays</param>
    /// <returns></returns>
    internal static List<List<string>> ReadArrayOfArraysOfStrings(this ref Utf8JsonReader reader, int numberOfArrays)
    {
        List<List<string>> arrayOfArrays = new List<List<string>>();

        // read array start of arrays
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new RdapBootstrapException("Invalid bootstrap JSON");
        }

        for (int i = 0; i < numberOfArrays; i++)
        {

            // read start of inner array
            reader.Read();
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new RdapBootstrapException("Invalid bootstrap JSON");
            }

            // read entries as strings
            var innerArray = new List<string>();
            do
            {
                reader.Read();
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }
                if (reader.TokenType == JsonTokenType.String)
                {
                    innerArray.Add(reader.GetString());
                    continue;
                }
                throw new RdapBootstrapException("Invalid bootstrap JSON");
            }
            while (true);
            arrayOfArrays.Add(innerArray);
        }

        // read end of outer array
        reader.Read();
        if (reader.TokenType != JsonTokenType.EndArray)
        {
            throw new RdapBootstrapException("Invalid bootstrap JSON");
        }

        return arrayOfArrays;
    }
}

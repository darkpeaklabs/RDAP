using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// RDAP service registry item converter
/// </summary>
public class RdapServiceRegistryItemConverter : JsonConverter<RdapServiceRegistryItem>
{
    /// <summary>
    /// Read RDAP service registry item
    /// </summary>
    /// <param name="reader">UTF-8 JSON reader reference</param>
    /// <param name="typeToConvert">Type to convert</param>
    /// <param name="options">JSON serializer options</param>
    /// <returns></returns>
    public override RdapServiceRegistryItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        /**
         "services": [
             [
               ["entry1", "entry2", "entry3"],
               [
                 "https://registry.example.com/myrdap/",
                 "http://registry.example.com/myrdap/"
               ]
             ],
             [
               ["entry4"],
               [
                 "http://example.org/"
               ]
             ]
           ]
         */

        var items = reader.ReadArrayOfArraysOfStrings(2);

        return new RdapServiceRegistryItem()
        {
            Entries = items[0],
            ServiceUrls = [.. items[1].Select(x => new Uri(x))]
        }; ;
    }

    /// <summary>
    /// Write RDAP service registry item
    /// </summary>
    /// <param name="writer">UTF-8 JSON writer</param>
    /// <param name="value">Item to write to JSON</param>
    /// <param name="options">JSON serializer options</param>
    public override void Write(Utf8JsonWriter writer, RdapServiceRegistryItem value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

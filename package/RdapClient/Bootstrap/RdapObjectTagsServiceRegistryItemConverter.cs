using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// RDAP service registry item for Provider Object Tags converter
/// </summary>
public class RdapObjectTagsServiceRegistryItemConverter : JsonConverter<RdapObjectTagsServiceRegistryItem>
{
    /// <summary>
    /// Read RDAP service registry item for Provider Object Tags 
    /// </summary>
    /// <param name="reader">UTF-8 JSON reader reference</param>
    /// <param name="typeToConvert">Type to convert</param>
    /// <param name="options">JSON serializer options</param>
    /// <returns></returns>
    public override RdapObjectTagsServiceRegistryItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        /*
           "services": [
            [
              ["contact@example.com"],
              ["YYYY"],
              [
                "https://example.com/rdap/"
              ]
            ],
            [
              ["contact@example.org"],
              ["ZZ54"],
              [
                "http://rdap.example.org/"
              ]
            ],
            [
              ["contact@example.net"],
              ["1754"],
              [
                "https://example.net/rdap/",
                "http://example.net/rdap/"
              ]
            ]
          ]
        */

        var items = reader.ReadArrayOfArraysOfStrings(3);
        var serviceUrls = items[2].Select(x =>
        {
            try
            {
                return new Uri(x);
            }
            catch(UriFormatException exception)
            {
                throw new RdapBootstrapException($"{x} is not a valid URI", exception);
            }

        }).ToList();

        return new RdapObjectTagsServiceRegistryItem()
        {
            Contacts = items[0],
            Identifiers = items[1],
            ServiceUrls = serviceUrls
        };
    }

    /// <summary>
    /// Write RDAP service registry item for Provider Object Tags
    /// </summary>
    /// <param name="writer">UTF-8 JSON writer</param>
    /// <param name="value">Item to write to JSON</param>
    /// <param name="options">JSON serializer options</param>
    public override void Write(Utf8JsonWriter writer, RdapObjectTagsServiceRegistryItem value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

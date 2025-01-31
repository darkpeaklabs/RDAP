using System;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// RDAP service registry base class
/// </summary>
public class RdapServiceRegistryBase
{
    /// <summary>
    /// corresponds to the format version of the registry
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; }

    /// <summary>
    /// the latest update date of the registry by IANA in the Internet date/time format[RFC3339]
    /// </summary>
    [JsonPropertyName("publication")]
    public DateTimeOffset Publication { get; set; }

    /// <summary>
    /// Contain a comment regarding the content of the bootstrap object.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }
}

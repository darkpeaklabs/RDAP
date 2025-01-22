using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// Represents RDAP service registry
/// </summary>
public class RdapServiceRegistry : RdapServiceRegistryBase
{
    /// <summary>
    /// List of registry services
    /// </summary>
    [JsonPropertyName("services")]
    public IReadOnlyCollection<RdapServiceRegistryItem> Services { get; set; }
}

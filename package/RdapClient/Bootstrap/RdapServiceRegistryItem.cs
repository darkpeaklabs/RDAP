using System;
using System.Collections.Generic;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// RDAP service registry item
/// </summary>
public class RdapServiceRegistryItem
{
    /// <summary>
    /// Entries that have the same set of base RDAP URLs
    /// </summary>
    public IReadOnlyCollection<string> Entries { get; set; }

    /// <summary>
    /// Base RDAP URLs usable for the entries
    /// </summary>
    public IReadOnlyCollection<Uri> ServiceUrls { get; set; }
}

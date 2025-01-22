using System;
using System.Collections.Generic;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// Bootstrap RDAP Service Registry item for Provider Object Tags
/// </summary>
public class RdapObjectTagsServiceRegistryItem
{
    /// <summary>
    /// Contact information for the registered service provider identifiers
    /// </summary>
    public IReadOnlyCollection<string> Contacts { get; set; }

    /// <summary>
    /// Alphanumeric identifiers that identify RDAP service providers
    /// </summary>
    public IReadOnlyCollection<string> Identifiers { get; set; }

    /// <summary>
    /// Base RDAP URLs usable for the entries
    /// </summary>
    public IReadOnlyCollection<Uri> ServiceUrls { get; set; }
}

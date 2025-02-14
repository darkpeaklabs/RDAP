﻿using System;
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
    public IReadOnlyList<string> Entries { get; set; }

    /// <summary>
    /// Base RDAP URLs usable for the entries
    /// </summary>
    public IReadOnlyList<Uri> ServiceUrls { get; set; }
}

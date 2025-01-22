using CsvHelper.Configuration.Attributes;
using System;

namespace DarkPeakLabs.Rdap.Utilities;

/// <summary>
/// Represent IANA Registrar ID registry entry
/// </summary>
public class IANARegistrar
{
    /// <summary>
    /// IANA assigned registrar ID
    /// </summary>
    [Name("ID")]
    public int Id { get; set; }

    /// <summary>
    /// Registrar name
    /// </summary>
    [Name("Registrar Name")]
    public string Name { get; set; }

    /// <summary>
    /// Status
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Base RDAP url
    /// </summary>
    [Name("RDAP Base URL")]
    [TypeConverter(typeof(UriConverter))]
    public Uri RdapBaseUrl { get; set; }
}

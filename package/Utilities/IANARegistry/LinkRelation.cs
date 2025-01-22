using CsvHelper.Configuration.Attributes;

namespace DarkPeakLabs.Rdap.Utilities;

/// <summary>
/// Represent IANA Link Relations registry entry
/// </summary>
public class LinkRelation
{
    /// <summary>
    /// Link relation name
    /// </summary>
    [Name("Relation Name")]
    public string Name { get; set; }

    /// <summary>
    /// Link relation description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Link relation reference
    /// </summary>
    public string Reference { get; set; }

    /// <summary>
    /// Link relation notes
    /// </summary>
    public string Notes { get; set; }
}

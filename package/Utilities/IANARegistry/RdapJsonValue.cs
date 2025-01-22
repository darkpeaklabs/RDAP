namespace DarkPeakLabs.Rdap.Utilities;

/// <summary>
/// Represent IANA RDAP JSON Values registry entry
/// </summary>
public class RdapJsonValue
{
    /// <summary>
    /// Value
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Value type
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Value description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Value registrant
    /// </summary>
    public string Registrant { get; set; }

    /// <summary>
    /// Value reference
    /// </summary>
    public string Reference { get; set; }
}

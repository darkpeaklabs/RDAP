using CsvHelper.Configuration.Attributes;

namespace DarkPeakLabs.Rdap.Utilities;

public class DnsSecDigestTypeValue
{
    [Name("Value")]
    public string Value { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string Reference { get; set; }
}

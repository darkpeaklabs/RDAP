using CsvHelper.Configuration.Attributes;

namespace DarkPeakLabs.Rdap.Utilities;

public class DnsSecAlgorithmNumber
{
    [Name("Number")]
    public string Value { get; set; }

    public string Description { get; set; }

    public string Mnemonic { get; set; }

    [Name("Zone\nSigning")]
    public string ZoneSigning { get; set; }

    [Name("Trans.\nSec.")]
    public string TransSec { get; set; }

    public string Reference { get; set; }
}

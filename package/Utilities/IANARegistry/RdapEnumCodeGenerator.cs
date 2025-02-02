using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DarkPeakLabs.Rdap.Values;

namespace DarkPeakLabs.Rdap.Utilities;

public static class RdapEnumCodeGenerator
{
    private static readonly Regex regexDescription = new("[\\r\\t\\n\\s]+");
    private static readonly Regex regexValue = new("[ -._/]?");
    private static readonly TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

    private static IEnumerable<RdapJsonValue> jsonValues;

    public static async Task<string> GenerateLinkRelationEnumAsync()
    {
        var type = typeof(RdapLinkRelationType);

        using IANARegistryClient client = new IANARegistryClient();
        var linkRelations = await client.GetLinkRelationsAsync().ConfigureAwait(false);

        StringBuilder sourceCode = new StringBuilder();
        sourceCode.AppendHeader(type);
        foreach (var linkRelation in linkRelations)
        {
            sourceCode.AppendValue(linkRelation.Name, linkRelation.Description);
        }

        sourceCode.AppendFooter();
        return sourceCode.ToString();
    }

    public static async Task<string> GenerateEntityRoleEnumAsync()
    {
        return await GenerateJsonValueEnumsAsync(typeof(RdapEntityRole), "role").ConfigureAwait(false);
    }

    public static async Task<string> GenerateEventActionEnumAsync()
    {
        return await GenerateJsonValueEnumsAsync(typeof(RdapEventAction), "event action").ConfigureAwait(false);
    }

    public static async Task<string> GenerateNoticeAndRemarkTypeEnumAsync()
    {
        return await GenerateJsonValueEnumsAsync(typeof(RdapNoticeAndRemarkType), "notice and remark type").ConfigureAwait(false);
    }

    public static async Task<string> GenerateStatusEnumAsync()
    {
        return await GenerateJsonValueEnumsAsync(typeof(RdapStatus), "status").ConfigureAwait(false);
    }

    public static void SaveAs(this string code, string path)
    {
        using StreamWriter writer = new StreamWriter(path);
        writer.Write(code);
    }

    public static async Task<string> GenerateDnsSecAlgorithmNumbersAsync()
    {
        using IANARegistryClient client = new IANARegistryClient();
        var numbers = await client.GetDnsSecAlgorithmNumbersAsync().ConfigureAwait(false);

        return GenerateDnsSecEnum<DnsSecAlgorithmType>(
            numbers.Select(x => new DnsSecEnumValue()
            {
                Value = x.Value,
                Description = x.Description
            }).ToList());
    }

    public static async Task<string> GenerateDnsSecDigestTypesAsync()
    {
        using IANARegistryClient client = new IANARegistryClient();
        var numbers = await client.GetDnsSecDigestTypesAsync().ConfigureAwait(false);

        return GenerateDnsSecEnum<DnsSecDigestType>(
            numbers.Select(x => new DnsSecEnumValue()
            {
                Value = x.Value,
                Description = x.Description
            }).ToList());
    }

    private static string GenerateDnsSecEnum<T>(List<DnsSecEnumValue> enumValues)
    {
        var type = typeof(T);
        StringBuilder sourceCode = new StringBuilder();
        sourceCode.AppendHeader(type);

        foreach (var enumValue in enumValues)
        {
            if (enumValue.Description.Equals("reserved", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            if (!int.TryParse(enumValue.Value, out var value))
            {
                continue;
            }

            var bracketIndex = enumValue.Description.IndexOf('(', StringComparison.Ordinal);
            var name = bracketIndex == -1 ? enumValue.Description : enumValue.Description[..bracketIndex];
            bool isReserved = name.StartsWith("reserved", StringComparison.OrdinalIgnoreCase);
            if (isReserved)
            {
                sourceCode.AppendLine("#pragma warning disable CA1700");
            }
            
            sourceCode.AppendValue(name, enumValue.Description, value);

            if (isReserved)
            {
                sourceCode.AppendLine("#pragma warning restore CA1700");
            }
        }

        sourceCode.AppendFooter();
        return sourceCode.ToString();
    }

    private static async Task<string> GenerateJsonValueEnumsAsync(Type type, string valueType)
    {
        if (jsonValues == null)
        {
            using IANARegistryClient client = new IANARegistryClient();
            jsonValues = await client.GetRdapJsonValuesAsync().ConfigureAwait(false);
        }

        StringBuilder sourceCode = new StringBuilder();
        sourceCode.AppendHeader(type);

        var typeJsonValues = jsonValues.Where(x => x.Type.Equals(valueType, StringComparison.OrdinalIgnoreCase)).ToList();

        if (typeJsonValues.Count == 0)
        {
            throw new ArgumentException($"Invalid value type");
        }

        foreach (var jsonValue in typeJsonValues)
        {
            sourceCode.AppendValue(jsonValue.Value, jsonValue.Description);
        }

        sourceCode.AppendFooter();
        return sourceCode.ToString();
    }

    private static string FormatDescription(string value)
    {
        value = regexDescription.Replace(value, " ");
        return value.Replace("\"", "\\\"", StringComparison.Ordinal);
    }

    private static string FormatValue(string value)
    {
        return regexValue.Replace(textInfo.ToTitleCase(value), "");
    }

    private static string FormatName(string value)
    {
        return textInfo.ToTitleCase(value);
    }

    private static void AppendHeader(this StringBuilder stringBuilder, Type type)
    {
        stringBuilder.AppendLine(CultureInfo.InvariantCulture, $"// Generated file from IANA registry ({DateTime.Now})");
        stringBuilder.AppendLine("using System.ComponentModel.DataAnnotations;");
        stringBuilder.AppendLine(CultureInfo.InvariantCulture, $"namespace {type.Namespace}");
        stringBuilder.AppendLine("{");

        stringBuilder.AppendLine(CultureInfo.InvariantCulture, $"\tpublic enum {type.Name}");
        stringBuilder.AppendLine("\t{");

        stringBuilder.AppendLine("\t\t/// <summary>");
        stringBuilder.AppendLine("\t\t/// Unknown value");
        stringBuilder.AppendLine("\t\t/// </summary>");
        stringBuilder.AppendLine("\t\t[Display(Name = \"Unknown Value\", Description = \"Server returned value not registered with IANA\")]");
        stringBuilder.AppendLine("\t\tUnknown = -1,");
    }
    private static void AppendFooter(this StringBuilder stringBuilder)
    {
        stringBuilder.AppendLine("\t}");
        stringBuilder.AppendLine("}");
    }

    private static void AppendValue(this StringBuilder stringBuilder, string name, string description, int? value = null)
    {
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("\t\t/// <summary>");
        stringBuilder.AppendLine(CultureInfo.InvariantCulture, $"\t\t/// {FormatDescription(description)}");
        stringBuilder.AppendLine("\t\t/// </summary>");
        stringBuilder.AppendLine(CultureInfo.InvariantCulture, $"\t\t[Display(Name = \"{FormatName(name)}\", Description = \"{FormatDescription(description)}\")]");

        string formattedValue = FormatValue(name);
        string valueLine = value.HasValue ?
            $"\t\t{formattedValue} = {value}," :
            $"\t\t{formattedValue},";

        if (formattedValue.Equals("reserved", StringComparison.OrdinalIgnoreCase))
        {
            stringBuilder.AppendLine("#pragma warning disable CA1700 // Do not name enum values 'Reserved'");
            stringBuilder.AppendLine(valueLine);
            stringBuilder.AppendLine("#pragma warning restore CA1700 // Do not name enum values 'Reserved'");

        }
        else 
        {
            stringBuilder.AppendLine(valueLine);
        }
    }
}

using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;

namespace DarkPeakLabs.Rdap.Utilities;

/// <summary>
/// Custom URI CSV field converter
/// </summary>
public class UriConverter : ITypeConverter
{
    /// <summary>
    /// Convert field text value to Uri
    /// </summary>
    /// <param name="text"></param>
    /// <param name="row"></param>
    /// <param name="memberMapData"></param>
    /// <returns></returns>
    public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        if (string.IsNullOrEmpty(text?.Trim()))
        {
            return null;
        }
        return new Uri(text);
    }

    /// <summary>
    /// Convert Uri to text value
    /// </summary>
    /// <param name="value"></param>
    /// <param name="row"></param>
    /// <param name="memberMapData"></param>
    /// <returns></returns>
    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        return value == null ? string.Empty : (value as Uri).AbsoluteUri;
    }
}

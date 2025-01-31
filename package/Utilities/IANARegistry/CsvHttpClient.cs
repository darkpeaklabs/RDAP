using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Utilities;

/// <summary>
/// Http client for reading and parsing CSV files
/// </summary>
public class CsvHttpClient : HttpClient
{
    /// <summary>
    /// Creates a new instance of <see cref="CsvHttpClient"/> class
    /// </summary>
    public CsvHttpClient() : base()
    {
        SetDefaultHeaders();
    }

    /// <summary>
    /// Creates a new instance of <see cref="CsvHttpClient"/> class
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    public CsvHttpClient(HttpMessageHandler handler) : base(handler)
    {
        SetDefaultHeaders();
    }

    /// <summary>
    /// Creates a new instance of <see cref="CsvHttpClient"/> class
    /// </summary>
    /// <param name="handler">HTTP message handler</param>
    /// <param name="disposeHandler">Dispose handler</param>
    public CsvHttpClient(HttpMessageHandler handler, bool disposeHandler) : base(handler, disposeHandler)
    {
        SetDefaultHeaders();
    }

    /// <summary>
    /// Get CSV file records
    /// </summary>
    /// <typeparam name="T">Record data type</typeparam>
    /// <param name="requestUri">Request Url</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<T>> GetRecordsAsync<T>(Uri requestUri)
    {
        using StreamReader reader = new StreamReader(await GetStreamAsync(requestUri).ConfigureAwait(false));
        using CsvReader csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        return csvReader.GetRecords<T>().ToList();
    }

    /// <summary>
    /// Get CSV file records
    /// </summary>
    /// <typeparam name="T">Record data type</typeparam>
    /// <param name="requestUri">Request Url</param>
    /// <returns></returns>
    public IReadOnlyList<T> GetRecords<T>(Uri requestUri)
    {
        return GetRecordsAsync<T>(requestUri).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Set default HTTP request headers
    /// </summary>
    private void SetDefaultHeaders()
    {
        DefaultRequestHeaders.Accept.Clear();
        DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
        DefaultRequestHeaders.Add("User-Agent", nameof(CsvHttpClient));
    }
}

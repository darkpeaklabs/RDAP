using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace Microsoft.CorporateDomains.Rdap.Test
{
    //From: https://github.com/PeterDaveHello/top-1m-domains?tab=readme-ov-file
    //-------------------------------------------------------------------------
    //Cisco Umbrella
    //Download page: https://s3-us-west-1.amazonaws.com/umbrella-static/index.html
    //Download link: https://s3-us-west-1.amazonaws.com/umbrella-static/top-1m.csv.zip
    //Majestic
    //Download page: https://majestic.com/reports/majestic-million
    //Download link: https://downloads.majestic.com/majestic_million.csv
    //BuiltWith
    //Download page: https://builtwith.com/top-1m
    //Download link: https://builtwith.com/dl/builtwith-top1m.zip
    //DomCop
    //Download page: https://www.domcop.com/top-10-million-websites
    //Download link: https://www.domcop.com/files/top/top10milliondomains.csv.zip
    //Tranco
    //Download page: https://tranco-list.eu/
    //Download link: https://tranco-list.eu/top-1m.csv.zip
    //Cloudflare
    //Download page: https://radar.cloudflare.com/domains
    //Download link: https://radar.cloudflare.com/charts/LargerTopDomainsTable/attachment?id=1257&top=1000000

    public abstract class DomainList<T> : IDomainList where T : IDomainListRecord
    {
        private const string CacheFolder = "domains";
        private readonly static TimeSpan _minAge = TimeSpan.FromDays(1);
        private readonly static string _basePath = Path.Combine(Path.GetTempPath(), CacheFolder);

        protected virtual string FileExtension { get; } = "csv";

        public abstract Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache);

        public async Task<IReadOnlyCollection<string>> GetDomainsAsync()
        {
            return await GetDomainsAsync(useCache: true).ConfigureAwait(false);
        }

        protected async Task<IReadOnlyCollection<string>> GetDomainsAsync(
            string sourceName,
            Uri downloadUri,
            bool hasHeaderRecord,
            bool useCache,
            string delimiter = ",")
        {
            string domainListPath = Path.Combine(_basePath, sourceName);

            DomainListMetadata metadata = null;
            FileInfo metadataFileInfo = new FileInfo(Path.Combine(domainListPath, $"metadata.json"));
            FileInfo dataFileInfo = null;

            if (useCache && metadataFileInfo.Exists)
            {
                metadata = await ReadDomainListMetadataAsync(metadataFileInfo.FullName).ConfigureAwait(false);
                dataFileInfo = new FileInfo(Path.Combine(domainListPath, metadata.Filename));
                if (dataFileInfo.Exists && dataFileInfo.Length > 0 && ((DateTime.UtcNow - metadata.LastModified) < _minAge))
                {
                    using var dataStream = File.OpenRead(dataFileInfo.FullName);
                    return await ReadDomainsAsync(
                        dataStream,
                        delimiter,
                        hasHeaderRecord).ConfigureAwait(false);
                }
            }

            using HttpClient client = CreateHttpClient();
            if (dataFileInfo != null && dataFileInfo.Exists)
            {
                client.DefaultRequestHeaders.IfModifiedSince = metadata.LastModified;
            }
            using var httpResponse = await client.GetAsync(downloadUri).ConfigureAwait(false);

            if (httpResponse.StatusCode == HttpStatusCode.NotModified)
            {
                metadata.LastModified = DateTime.UtcNow;
                await WriteDomainListMetadataAsync(metadataFileInfo.FullName, metadata).ConfigureAwait(false);

                using var dataStream = File.OpenRead(dataFileInfo.FullName);
                return await ReadDomainsAsync(
                    dataStream,
                    delimiter,
                    hasHeaderRecord).ConfigureAwait(false);
            }

            DateTimeOffset? lastModified = httpResponse.Content.Headers.LastModified ??
                throw new DomainListException("Last-Modified header not found");
            var contentDispositionHeader = httpResponse.Content.Headers.ContentDisposition;
            metadata = new DomainListMetadata()
            {
                LastModified = lastModified.Value, // ?? DateTime.UtcNow,
                Filename = contentDispositionHeader?.FileName ?? $"data.{FileExtension}",
            };

            if (!Directory.Exists(domainListPath))
            {
                Directory.CreateDirectory(domainListPath);
            }

            using var fileStream = File.Open(Path.Combine(domainListPath, metadata.Filename), FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            await httpResponse.Content.CopyToAsync(fileStream).ConfigureAwait(false);

            await WriteDomainListMetadataAsync(metadataFileInfo.FullName, metadata).ConfigureAwait(false);

            fileStream.Position = 0;
            return await ReadDomainsAsync(
                fileStream,
                delimiter,
                hasHeaderRecord).ConfigureAwait(false);
        }

        protected virtual async Task<IReadOnlyCollection<string>> ReadDomainsAsync(Stream stream, string delimiter, bool hasHeaderRecord)
        {
            HashSet<string> list = [];

            using StreamReader reader = new StreamReader(stream);
            using var csv = new CsvReader(
                reader,
                new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = delimiter,
                    Encoding = Encoding.UTF8,
                    HasHeaderRecord = hasHeaderRecord
                });

            await foreach (var record in csv.GetRecordsAsync<T>())
            {
                list.Add(record.Domain);
            }

            return list;
        }

        private static async Task<DomainListMetadata> ReadDomainListMetadataAsync(string path)
        {
            using StreamReader reader = new StreamReader(path);
            return JsonSerializer.Deserialize<DomainListMetadata>(
                await reader.ReadToEndAsync().ConfigureAwait(false));
        }

        private static async Task WriteDomainListMetadataAsync(string path, DomainListMetadata metadata)
        {
            using StreamWriter writer = new StreamWriter(path);
            await writer.WriteAsync(JsonSerializer.Serialize(metadata)).ConfigureAwait(false);
        }

        private static HttpClient CreateHttpClient()
        {
            return new HttpClient();
        }
    }
}

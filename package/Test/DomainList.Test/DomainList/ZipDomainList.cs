using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.CorporateDomains.Rdap.Test
{
    public abstract class ZipDomainList<T> : DomainList<T> where T : IDomainListRecord
    {
        protected override string FileExtension => "zip";

        protected override async Task<IReadOnlyCollection<string>> ReadDomainsAsync(Stream stream, string delimiter, bool hasHeaderRecord)
        {
            var memoryStream = new MemoryStream();
            using var zip = new ZipArchive(stream, ZipArchiveMode.Read);
            var entry = zip.Entries.First();
            using Stream entryStream = entry.Open();
            byte[] buffer = new byte[2048];
            int bytesRead = 0;
            do
            {
                bytesRead = await entryStream.ReadAsync(buffer).ConfigureAwait(false);
                await memoryStream.WriteAsync(new ReadOnlyMemory<byte>(buffer, 0, bytesRead)).ConfigureAwait(false);
            }
            while (bytesRead > 0);
            memoryStream.Position = 0;

            return await base.ReadDomainsAsync(memoryStream, delimiter, hasHeaderRecord).ConfigureAwait(false);
        }
    }
}

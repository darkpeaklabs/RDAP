using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.CorporateDomains.Rdap.Test
{
    public class MajesticDomainList : DomainList<MajesticRecord>
    {
        public override async Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache)
        {
            return await GetDomainsAsync(
                "majestic",
                new Uri("https://downloads.majestic.com/majestic_million.csv"),
                hasHeaderRecord: true,
                useCache)
            .ConfigureAwait(false);
        }
    }
}

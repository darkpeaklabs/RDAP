using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.CorporateDomains.Rdap.Test
{
    public class TrancoDomainList : ZipDomainList<CiscoUmbrellaRecord>
    {
        public override async Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache)
        {
            return await GetDomainsAsync(
                "tranco",
                new Uri("https://tranco-list.eu/top-1m.csv.zip"),
                hasHeaderRecord: false,
                useCache)
            .ConfigureAwait(false);
        }
    }
}

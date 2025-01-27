using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Utilities
{
    public class DomCopDomainList : ZipDomainList<CiscoUmbrellaRecord>
    {
        public override async Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache)
        {
            return await GetDomainsAsync(
                "domcop",
                new Uri("https://www.domcop.com/files/top/top10milliondomains.csv.zip"),
                hasHeaderRecord: true,
                useCache)
            .ConfigureAwait(false);
        }
    }
}

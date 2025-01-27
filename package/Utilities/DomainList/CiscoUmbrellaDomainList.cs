using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Utilities
{
    public class CiscoUmbrellaDomainList : ZipDomainList<CiscoUmbrellaRecord>
    {
        public override async Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache)
        {
            return await GetDomainsAsync(
                "cisco-umbrella",
                new Uri("https://s3-us-west-1.amazonaws.com/umbrella-static/top-1m.csv.zip"),
                hasHeaderRecord: false,
                useCache)
            .ConfigureAwait(false);
        }
    }
}

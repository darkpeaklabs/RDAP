using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.CorporateDomains.Rdap.Test
{
    public class BuiltWithDomainList : ZipDomainList<CiscoUmbrellaRecord>
    {
        public override async Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache)
        {
            return await GetDomainsAsync(
                "builtwith",
                new Uri("https://builtwith.com/dl/builtwith-top1m.zip"),
                hasHeaderRecord: false,
                useCache)
            .ConfigureAwait(false);
        }
    }
}

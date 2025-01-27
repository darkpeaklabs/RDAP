using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Utilities
{
    public class CloudFlareDomainList : DomainList<CloudFlareRecord>
    {
        public override async Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache)
        {
            return await GetDomainsAsync(
                "cloudflare",
                new Uri("https://radar.cloudflare.com/charts/LargerTopDomainsTable/attachment?id=1257&top=1000000"),
                hasHeaderRecord: true,
                useCache)
            .ConfigureAwait(false);
        }
    }
}

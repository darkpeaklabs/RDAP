using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.CorporateDomains.Rdap.Test;

public interface IDomainList
{
    Task<IReadOnlyCollection<string>> GetDomainsAsync();
    Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache);
}

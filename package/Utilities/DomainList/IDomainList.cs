using System.Collections.Generic;
using System.Threading.Tasks;

namespace DarkPeakLabs.Rdap.Utilities;

public interface IDomainList
{
    Task<IReadOnlyCollection<string>> GetDomainsAsync();
    Task<IReadOnlyCollection<string>> GetDomainsAsync(bool useCache);
}

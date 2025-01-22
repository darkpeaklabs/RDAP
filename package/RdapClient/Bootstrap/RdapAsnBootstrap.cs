using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DarkPeakLabs.Rdap.Bootstrap;

public class RdapAsnBootstrap : RdapBootstrap
{
    public RdapAsnBootstrap(ILogger logger = null): base(logger)
    {

    }

    public override Task<Uri> FindServiceUrlAsync(string value)
    {
        _ = value ?? throw new ArgumentNullException(nameof(value));

        throw new NotImplementedException();
    }
}

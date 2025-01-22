using System;

namespace DarkPeakLabs.Rdap.Bootstrap;

/// <summary>
/// RDAP bootstrap exception
/// </summary>
public class RdapBootstrapException : Exception
{
    internal RdapBootstrapException(string message) : base(message)
    {
    }

    public RdapBootstrapException()
    {
    }

    public RdapBootstrapException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

using System;

namespace DarkPeakLabs.Rdap;

/// <summary>
/// RDAP HTTP exception
/// </summary>
public class RdapClientTimeoutException : RdapException
{
    public RdapClientTimeoutException()
    {
    }

    public RdapClientTimeoutException(string message) : base(message)
    {
    }

    public RdapClientTimeoutException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

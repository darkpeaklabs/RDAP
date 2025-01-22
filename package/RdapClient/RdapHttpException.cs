using System;

namespace DarkPeakLabs.Rdap;

/// <summary>
/// RDAP HTTP exception
/// </summary>
public class RdapHttpException : RdapException
{
    public RdapHttpException()
    {
    }

    public RdapHttpException(string message) : base(message)
    {
    }

    public RdapHttpException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

using System;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// RDAP exception
    /// </summary>
    public class RdapException : Exception
    {
        public RdapException(string message) : base(message)
        {
        }

        public RdapException()
        {
        }

        public RdapException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

using System;

namespace Microsoft.CorporateDomains.Rdap.Test
{
    [Serializable]
    public class DomainListException : Exception
    {
        public DomainListException()
        {
        }

        public DomainListException(string message) : base(message)
        {
        }

        public DomainListException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

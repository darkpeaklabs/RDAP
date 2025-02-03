using System;

namespace DarkPeakLabs.Rdap
{
    [Serializable]
    public class RdapSerializerException : RdapException
    {
        public string ResponseBody { get; }

        public RdapSerializerException()
        {
        }

        public RdapSerializerException(string message) : base(message)
        {
        }

        public RdapSerializerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RdapSerializerException(string message, string responseBody, Exception innerException) : this(message, innerException)
        {
            ResponseBody = responseBody;
        }
    }
}

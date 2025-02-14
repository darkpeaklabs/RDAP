﻿using System;

namespace DarkPeakLabs.Rdap.Utilities
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

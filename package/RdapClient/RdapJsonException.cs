using System;
using System.Text.Json;

namespace DarkPeakLabs.Rdap
{
    /// <summary>
    /// RDAP JSON exception
    /// </summary>
    public class RdapJsonException : RdapException
    {
        public string Json { get; private set; }

        public JsonTokenType? TokenType { get; private set; }
        public int? CurrentDepth { get; private set; }
        public long? TokenStartIndex { get; private set; }
        public long? BytesConsumed { get; private set; }
        public SequencePosition? Position { get; private set; }
        public JsonReaderState? CurrentState { get; private set; }

        public RdapJsonException(string message) : base(message)
        {
        }

        internal RdapJsonException(string message, ref Utf8JsonReader reader) : base(message)
        {
            TokenType = reader.TokenType;
            CurrentDepth = reader.CurrentDepth;
            TokenStartIndex = reader.TokenStartIndex;
            BytesConsumed = reader.BytesConsumed;
            Position = reader.Position;
            CurrentState = reader.CurrentState;
        }

        public RdapJsonException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public RdapJsonException(string message, string json, Exception innerException) : base(message, innerException)
        {
            Json = json;
        }

        public RdapJsonException()
        {
        }
    }
}
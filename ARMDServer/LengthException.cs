using System;
using System.Runtime.Serialization;

namespace ARMDServer
{
    [Serializable]
    public class SpanLengthException : Exception
    {
        public SpanLengthException() { }
        public SpanLengthException(string message) : base(message) { }
        public SpanLengthException(string message, Exception inner) : base(message, inner) { }
        protected SpanLengthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
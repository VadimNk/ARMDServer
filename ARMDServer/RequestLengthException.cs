using System;
using System.Runtime.Serialization;

namespace ARMDServer
{
    [Serializable]
    public class RequestLengthException : Exception
    {
        public RequestLengthException() { }
        public RequestLengthException(string message) : base(message) { }
        public RequestLengthException(string message, Exception inner) : base(message, inner) { }
        protected RequestLengthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
using System;
using System.Runtime.Serialization;

namespace ARMDServer
{
    [Serializable]
    public class BinaryDateTimeLengthException : Exception
    {
        public BinaryDateTimeLengthException() { }
        public BinaryDateTimeLengthException(string message) : base(message) { }
        public BinaryDateTimeLengthException(string message, Exception inner) : base(message, inner) { }
        protected BinaryDateTimeLengthException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
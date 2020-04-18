[System.Serializable]
public class BinaryDateTimeLengthException : System.Exception
{
    public BinaryDateTimeLengthException() { }
    public BinaryDateTimeLengthException(string message) : base(message) { }
    public BinaryDateTimeLengthException(string message, System.Exception inner) : base(message, inner) { }
    protected BinaryDateTimeLengthException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
[System.Serializable]
public class RequestLengthException : System.Exception
{
    public RequestLengthException() { }
    public RequestLengthException(string message) : base(message) { }
    public RequestLengthException(string message, System.Exception inner) : base(message, inner) { }
    protected RequestLengthException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
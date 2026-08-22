public abstract class AppException : Exception
{
    public int StatusCode { get; }
    public string? MessageKey { get; }
    protected AppException(string message,int statusCode,string? messageKey = null) : base(message)
    {
        StatusCode=statusCode;
        MessageKey=messageKey;
    }
}
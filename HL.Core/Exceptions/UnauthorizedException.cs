public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message,string? messageKey=null):base(message,401,messageKey)
    {
        
    }
}
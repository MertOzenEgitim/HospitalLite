public class NotFoundException : AppException
{
    public NotFoundException(string message,string? messageKey=null):base(message,404,messageKey)
    {
        
    }
}
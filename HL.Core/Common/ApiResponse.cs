namespace HL.Core.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? MessageKey { get; set; }
    public int StatusCode { get; set; }

    public static ApiResponse<T> SuccessResponse(T data,string?message=null,string?messageKey=null,int statusCode=200)=>new ApiResponse<T>
    {
        Success=true,
        Data=data,
        Message=message,
        MessageKey=messageKey,
        StatusCode=statusCode
    };

    public static ApiResponse<T> FailResponse(string message,string messageKey,int statusCode=400)=>new ApiResponse<T>
    {
        Success=false,
        Data=default,
        Message=message,
        MessageKey=messageKey,
        StatusCode=statusCode
    };
}
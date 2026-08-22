using System.Text.Json;
using HL.Core.Common;

namespace HL.API.Middleware;

// Pipeline'ın en dış kabuğu. Altındaki HER ŞEYİ (controller dâhil) try/catch ile
// sarar; nerede exception fırlarsa buraya düşer, tek tip ApiResponse zarfına çevrilir.
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Bir sonraki middleware'e / controller'a geç.
            await _next(context);
        }
        catch (AppException ex)
        {
            // Bizim tanıdığımız, beklenen hata (401, 404, ...).
            // Kendi status'u + kendi messageKey'i ile döner. Mesaj güvenli.
            await WriteResponseAsync(context, ex.StatusCode, ex.Message, ex.MessageKey);
        }
        catch (Exception)
        {
            // Beklenmedik / gerçek hata (DB çöktü, null referans vb.).
            // Detayı istemciye SIZDIRMA — genel mesaj + sabit key.
            await WriteResponseAsync(context, 500, "Beklenmeyen bir hata oluştu.", "general.unexpected_error");
        }
    }

    private static async Task WriteResponseAsync(HttpContext context, int statusCode, string message, string? messageKey)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        // Başarı cevaplarıyla AYNI zarf: { success, data, message, messageKey, statusCode }.
        var response = ApiResponse<string>.FailResponse(message, messageKey, statusCode);

        // JSON'da camelCase — controller'ların döndüğü zarfla tutarlı olsun.
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}
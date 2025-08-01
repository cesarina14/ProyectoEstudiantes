using EscuelaPrimaria.Service;
using EscuelaPrimaria.Service.NewFolder;

namespace EscuelaPrimaria.Utitlity
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggingService _loggingService;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggingService loggingService)
        {
            _next = next;
            _loggingService = loggingService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Excepción no controlada capturada en middleware", ex);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var result = System.Text.Json.JsonSerializer.Serialize(new { error = "Error interno del servidor." });
                await context.Response.WriteAsync(result);
            }
        }
    }
}

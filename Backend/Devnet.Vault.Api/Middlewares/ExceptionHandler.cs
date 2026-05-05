using System.Net;
using System.Text.Json;

namespace Devnet.Vault.Api.Middlewares;

public class ExceptionHandler(RequestDelegate _next, ILogger<ExceptionHandler> _logger)
{
    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next.Invoke(context);

        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception e)
    {
        try
        {
            _logger.LogError(e, "Unhandled exception occurred while processing {Path}", context.Request.Path);
            if (!context.Response.HasStarted) // If response header not sent to client then rewrite the response
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var json = JsonSerializer.Serialize(e.Message, _jsonOptions);

                await context.Response.WriteAsync(json);
            }

        }
        catch (Exception failedExecptionHandler)
        {
            _logger.LogCritical(failedExecptionHandler, "Failed to handle exception for {Path}", context.Request.Path);
        }
    }
}

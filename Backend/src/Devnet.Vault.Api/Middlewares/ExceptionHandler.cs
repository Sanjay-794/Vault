using FluentValidation;
using System.Text.Json;

namespace Devnet.Vault.Api.Middlewares;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex, "Validation failed");

            await WriteResponse(context, StatusCodes.Status400BadRequest, "Validation failed",
                ex.Errors.Select(x => new
                {
                    field = x.PropertyName,
                    error = x.ErrorMessage
                }));
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "Request cancelled");

            await WriteResponse(context, StatusCodes.Status499ClientClosedRequest,
                "Operation Cancelled by client", ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception");

            await WriteResponse(context, StatusCodes.Status500InternalServerError,
                "An error occurred", ex.Message);
        }
    }

    private static async Task WriteResponse(HttpContext context, int statusCode, string message, object error)
    {
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();

        context.Response.StatusCode = statusCode;

        context.Response.ContentType = "application/json";

        var response = new
        {
            message,
            error
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
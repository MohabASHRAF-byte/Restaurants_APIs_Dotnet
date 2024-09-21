using System.Text.Json;
using Restaurants.Domain.Exceptions;

namespace Restaurants.API.MiddleWares;

public class GlobalErrorHandling(
    ILogger<GlobalErrorHandling> logger
) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ResourseNotFound ex)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message
            };

            var jsonResponse = JsonSerializer.Serialize(errorResponse);

            await context.Response.WriteAsync(jsonResponse);

            logger.LogWarning(ex.Message);
        }
        catch (ForBidenException ex)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Unauthorized access is denied.");

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected error occurred.");
        }
    }
}
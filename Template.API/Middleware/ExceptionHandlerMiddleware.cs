using Common.SharedClasses.Exceptions;

namespace TripPlanner.API.Middlewares;

public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            logger.LogWarning(notFound.Message);

            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notFound.Message);
        }
        catch (ForbiddenException forbidden)
        {
            logger.LogWarning(forbidden.Message);

            context.Response.StatusCode = 403;
            await context.Response.WriteAsync(forbidden.Message);
        }
        catch (BadRequestException bre)
        {
            logger.LogWarning(bre.Message);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(bre.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
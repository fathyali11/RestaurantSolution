using Restaurats.Domain.Exceptions;
namespace Restaurats.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware>logger) : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger= logger;
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch(NotFoundException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(ex.Message);
        }
        catch(ForbiddenException)
        {
            context.Response.StatusCode=StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("forbidden request");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}

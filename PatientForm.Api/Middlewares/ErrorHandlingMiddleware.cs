using PatientForm.Infrastructure.Exceptions;

namespace PatientForm.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException e)
        {
            logger.LogError(e, e.Message);
            
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("The resource you are looking for can't be found");            
        }
        catch (Exception e)
        {
            logger.LogCritical(e, e.Message);
            
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An error as occured");
        }
    }
}
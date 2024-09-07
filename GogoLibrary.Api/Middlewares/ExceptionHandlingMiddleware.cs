using System.Net;
using GogoLibrary.Domain.Result;
using ILogger = Serilog.ILogger;

namespace GogoLibrary.Api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, 
        ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.Error(ex, ex.Message);

        var errorMessage = ex.Message;
        var response = ex switch
        {
            UnauthorizedAccessException => new BaseResult()
                { ErrorMessage = errorMessage, ErrorCode = (int)HttpStatusCode.Unauthorized },
            ArgumentNullException => new BaseResult() 
                { ErrorMessage = errorMessage, ErrorCode = (int)HttpStatusCode.BadRequest },
            _ => new BaseResult() { ErrorMessage = errorMessage, ErrorCode = (int)HttpStatusCode.InternalServerError },
        };
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.ErrorCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
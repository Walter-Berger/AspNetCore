namespace AccountService.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException validationException)
        {
            _logger.LogError(validationException.Message);
            await HandleValidationExceptionAsync(context, validationException);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        // map exceptions to their corresponding status code
        httpContext.Response.StatusCode = exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            DatabaseException => StatusCodes.Status500InternalServerError,
            DuplicationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        // error title and rfc link will be generated automatically depending on the status code
        var result = Results.Problem(
            statusCode: httpContext.Response.StatusCode,
            detail: exception?.Message,
            instance: $"{exception?.Source} - {httpContext.Request.Method}:{httpContext.Request.Path}"
        );

        // return error details
        await result.ExecuteAsync(httpContext);
    }    
    
    private static async Task HandleValidationExceptionAsync(HttpContext httpContext, ValidationException validationException)
    {
        var keys = validationException.Errors.Select(e => e.PropertyName).Distinct();
        var errors = keys.ToDictionary(key => key, key => validationException.Errors
            .Where(x => x.PropertyName == key)
            .Select(e => e.ErrorMessage)
            .ToArray());

        // error title and rfc link will be generated automatically depending on the status code
        var result = Results.ValidationProblem(
            statusCode: StatusCodes.Status400BadRequest,
            detail: "Validation Failed.",
            instance: $"{validationException?.Source} - {httpContext.Request.Method}:{httpContext.Request.Path}",
            errors: errors
        );

        // return validation error details
        await result.ExecuteAsync(httpContext);
    }
}

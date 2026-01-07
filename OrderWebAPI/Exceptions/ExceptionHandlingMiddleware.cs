using Microsoft.AspNetCore.Mvc;

namespace OrderWebAPI.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware( RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception");

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred"
            };

            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Title = "Bad Request";
                    problemDetails.Detail = exception.Message;
                    break;

                case KeyNotFoundException:
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Title = "Not Found";
                    problemDetails.Detail = exception.Message;
                    break;

                default:
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Title = "Internal Server Error";
                    problemDetails.Detail = "An unexpected error occurred.";
                    break;
            }

            context.Response.StatusCode = problemDetails.Status.Value;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}


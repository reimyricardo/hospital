using Clinic.Business.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Clinic.Api.ExceptionHandler;
internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception Occurred: {message}", exception.Message);

        ProblemDetails problemDetails = new ProblemDetails()
        {
            Status = GetStatusCode(exception),
            Title = GetTitle(exception),
            Detail = GetDetail(exception),
            Type = GetType(exception),
            Extensions = new Dictionary<string, object?>
            {
                {"validationErrors", GetValidationErrors(exception) }
            }
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;

        static string GetTitle(Exception exception)
            => exception switch
            {
                ValidationException => "Bad Request",
                _ => "Internal Server Error"
            };

        static string GetDetail(Exception exception)
            => exception switch
            {
                ValidationException validationException => validationException.Message,
                _ => exception.Message
            };

        static int GetStatusCode(Exception exception)
            => exception switch
            {
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };

        static string GetType(Exception exception)
            => exception switch
            {
                ValidationException => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };

        static object GetValidationErrors(Exception exception)
            => exception switch
            {
                ValidationException validationException => validationException.Errors,
                _ => []
            };
    }
}

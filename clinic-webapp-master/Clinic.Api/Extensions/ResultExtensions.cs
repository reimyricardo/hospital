using Clinic.Data.Entities.Common.Primitives;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using IResult = Clinic.Data.Contracts.IResult;

namespace Clinic.Api.Extensions;

public static class ResultExtensions
{
    public static ObjectResult ToProblemDetails(this IResult result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        ProblemDetails problemDetails = new ProblemDetails()
        {
            Status = GetStatusCode(result.Error.ErrorType),
            Title = GetTitle(result.Error.ErrorType),
            Detail = result.Error.ErrorDescription,
            Type = GetType(result.Error.ErrorType),
            Extensions = new Dictionary<string, object?>
            {
                {"error" , result.Error  },
                { "validationErrors", result.ValidationErrors ?? [] }
            }
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

        static int GetStatusCode(ErrorType errorType)
            => errorType switch
            {
                ErrorType.Validation => (int)HttpStatusCode.BadRequest,
                ErrorType.NotFound => (int)HttpStatusCode.NotFound,
                ErrorType.Conflit => (int)HttpStatusCode.Conflict,
                _ => (int)HttpStatusCode.InternalServerError
            };

        static string GetTitle(ErrorType errorType)
            => errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "NotFound",
                ErrorType.Conflit => "Conflit",
                _ => "Internal Server Error"
            };

        static string GetType(ErrorType errorType)
            => errorType switch
            {
                ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                ErrorType.Conflit => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };
    }
}

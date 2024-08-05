using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = [];
    }
    public Error[] Errors { get;}

    public ValidationException(Error[] validationErrors)
        : this()
    {
        Errors = validationErrors;
    }
}

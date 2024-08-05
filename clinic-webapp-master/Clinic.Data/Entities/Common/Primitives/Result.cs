using Clinic.Data.Contracts;

namespace Clinic.Data.Entities.Common.Primitives;

public sealed class Result : IResult
{
    private Result(bool isSucess,Error error, Error[]? validationErrors)
    {
        if (isSucess && error != Error.None
            || !isSucess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error {error}",nameof(error));
        }

        IsSuccess = isSucess;
        Error = error;
        IsFailure = !isSucess;
        ValidationErrors = validationErrors;
    }

    public Error Error { get; }

    public Error[]? ValidationErrors { get; }

    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public static Result Success() => new Result(true, Error.None,null);

    public static Result Failure(Error error) => new Result(false, error,null);

    public static Result ValidationFailure(Error[] validationErrors) => new Result(false, Error.ValidationError,validationErrors);
}

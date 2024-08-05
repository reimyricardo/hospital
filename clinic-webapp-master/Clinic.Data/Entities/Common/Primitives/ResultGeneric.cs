using Clinic.Data.Contracts;

namespace Clinic.Data.Entities.Common.Primitives;

public class Result<TData> : IResult
    where TData : class
{
    public Result(TData? data,Error error,bool isSucess, Error[]? validationErrors)
    {
        if (isSucess && error != Error.None
            || !isSucess && error == Error.None)
        {
            throw new ArgumentException("Invalid Error {error}", nameof(error));
        }

        Data = data;
        Error = error;
        ValidationErrors = validationErrors;
        IsSuccess = isSucess;
        IsFailure = !isSucess;
    }

    public TData? Data { get; }

    public Error Error { get; }

    public Error[]? ValidationErrors { get; }

    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public static Result<TData> Sucess(TData data) => new(data,Error.None,true,null);

    public static Result<TData> Failure(Error error) => new(default,error,false,null);

    public static Result<TData> ValidationFailure(Error[] validationErrors) => new(default, Error.ValidationError, false, validationErrors); 
}

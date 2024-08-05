namespace Clinic.Data.Entities.Common.Primitives;

public sealed class Error
{
    private Error(string erroCode, string errorDescription, ErrorType errorType)
    {
        ErrorCode = erroCode;
        ErrorDescription = errorDescription;
        ErrorType = errorType;
    }

    public string ErrorCode { get; init; }

    public string ErrorDescription { get; init; }

    public ErrorType ErrorType { get; init; }

    public static readonly Error None = new Error(string.Empty,string.Empty,ErrorType.None);

    public static readonly Error ValidationError = new Error("Validation.Error", "One or more validation failures have occurred", ErrorType.Validation);

    public static Error NotFound(string errorCode, string errorDescription) => new(errorCode,errorDescription,ErrorType.NotFound);

    public static Error Validation(string errorCode, string errorDescription) => new(errorCode, errorDescription, ErrorType.Validation);

    public static Error Conflit(string errorCode, string errorDescription) => new(errorCode, errorDescription, ErrorType.Conflit);
}

public enum ErrorType
{
    None = 0,
    Validation = 1,
    NotFound = 2,
    Conflit = 3
}
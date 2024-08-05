using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts;

public interface IResult
{
    public bool IsSuccess { get; }

    public bool IsFailure { get; }

    public Error Error { get; }

    public Error[]? ValidationErrors { get; }
}

namespace Clinic.Data.Contracts;

using MediatR;

public interface IQuery<out TResponse> : IRequest<TResponse> { }

public interface ICacheQuery { }

public interface ICacheQuery<out TResponse> : IQuery<TResponse>
{
    public string cacheKey { get; }

    public TimeSpan expirationTime { get; }
}



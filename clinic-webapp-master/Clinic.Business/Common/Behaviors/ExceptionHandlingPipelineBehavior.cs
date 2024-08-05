using MediatR;
using Microsoft.Extensions.Logging;

namespace Clinic.Business.Common.Behaviors;

internal sealed class ExceptionHandlingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> _logger;

    public ExceptionHandlingPipelineBehavior(ILogger<ExceptionHandlingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try 
        {
            return await next();
            
        } catch (Exception ex)
        {
            string requestName = typeof(TRequest).Name;

            _logger.LogError("Unhandled Exception for {requestName} with error message: {errorMessage}",requestName,ex.Message);

            throw;
        }
    }
}

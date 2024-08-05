using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Clinic.Business.Common.Behaviors;

internal sealed class RequestPerfomancePipelineBehavior<TRequest,TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<RequestPerfomancePipelineBehavior<TRequest, TResponse>> _logger;
    private readonly Stopwatch _stopwatch;

    public RequestPerfomancePipelineBehavior(ILogger<RequestPerfomancePipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
        _stopwatch = new Stopwatch();
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();

        TResponse response = await next();

        _stopwatch.Stop();

        long elapsedMilliseconds = _stopwatch.ElapsedMilliseconds;

        string requestName = typeof(TRequest).Name;

        _logger.LogInformation("The current request {requestName} complete in {elapsedMilliseconds} ms",requestName,elapsedMilliseconds);

        return response;
    }
}

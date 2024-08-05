using Clinic.Data.Entities.Common.Primitives;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Reflection;
using ValidationException = Clinic.Business.Common.Exceptions.ValidationException;

namespace Clinic.Business.Common.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest,TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationFailure[] validationFailures = await ValidateAsync(request, cancellationToken);

        if (validationFailures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type resultType = typeof(TResponse).GetGenericArguments()[0];

            MethodInfo? failureMethod = typeof(Result<>)
                                        .MakeGenericType(resultType)
                                        .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
            {
                #pragma warning disable CS8600 
                #pragma warning disable CS8603 
                return (TResponse)failureMethod
                                 .Invoke(
                                  null,
                                  [CreateValidationErrors(validationFailures)]);            }
        }

        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.ValidationFailure(CreateValidationErrors(validationFailures));
        }

        throw new ValidationException(CreateValidationErrors(validationFailures));
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request,CancellationToken cancellationToken = default)
    {
        if (!_validator.Any())
        {
            return [];
        }

        ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

        ValidationResult[] validationResults = 
            await Task.WhenAll(_validator.Select(validator => validator.ValidateAsync(request,cancellationToken)));


        ValidationFailure[] validationFailures = validationResults
                                .Where(validationResults => !validationResults.IsValid)
                                .SelectMany(validationResult => validationResult.Errors)
                                .ToArray();

        return validationFailures;
    }

    private Error[] CreateValidationErrors(ValidationFailure[] validationFailures) => 
        validationFailures.Select(validationErrors => Error.Validation(validationErrors.PropertyName,validationErrors.ErrorMessage)).ToArray();
}

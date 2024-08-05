using Clinic.Data.Options;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Clinic.Data.Validators;

public sealed class DatabaseOptionsValidator : IValidateOptions<DatabaseOptions>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public DatabaseOptionsValidator(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }


    public ValidateOptionsResult Validate(string? name, DatabaseOptions options)
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IValidator<DatabaseOptions> validator = scope.ServiceProvider.GetRequiredService<IValidator<DatabaseOptions>>();

        ValidationResult result = validator.Validate(options);

        if (result.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        string[] errors = result.Errors.Select(x => $"Validation failed to {x.PropertyName} with the Error Message {x.ErrorMessage}").ToArray();

        return ValidateOptionsResult.Fail(errors);
    }
}

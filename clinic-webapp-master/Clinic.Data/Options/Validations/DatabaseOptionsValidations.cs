using FluentValidation;

namespace Clinic.Data.Options.Validations;

public class DatabaseOptionsValidations : AbstractValidator<DatabaseOptions>
{
    public DatabaseOptionsValidations()
    {
        RuleFor(options => options.ConnectionString).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(options => options.ConnectionString).NotNull().WithMessage("The {PropertyName} cant be null");

        RuleFor(options => options.CommandTimeout).NotNull().WithMessage("The {PropertyName} cant be null");

        RuleFor(options => options.CommandTimeout).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(options => options.EnableDetailedErrors).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(options => options.EnableDetailedErrors).NotNull().WithMessage("The {PropertyName} cant be null");

        RuleFor(options => options.EnableSensitiveDataLogging).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(options => options.EnableSensitiveDataLogging).NotNull().WithMessage("The {PropertyName} cant be null");

        RuleFor(options => options.MaxRetryCount).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(options => options.MaxRetryCount).NotNull().WithMessage("The {PropertyName} cant be null");
    }
}

using FluentValidation;

namespace Clinic.Business.PersonsAddress.Commands.UpdatePersonAddress;

public class UpdatePersonAddressCommandValidator : AbstractValidator<UpdatePersonAddressCommand>
{
    public UpdatePersonAddressCommandValidator()
    {
        RuleFor(x => x.addressId)
            .NotNull().WithMessage("The address id cant be null")
            .GreaterThan(0).WithMessage("The address id cant be zero or lower than zero");

        RuleFor(x => x.streetNumber)
           .NotNull().WithMessage("The street number cant be null")
           .GreaterThan(0).WithMessage("The street number cant be zero or lower than zero");

        RuleFor(x => x.addressLine1)
            .NotNull().WithMessage("The address line 1 cant be null")
            .NotEmpty().WithMessage("The address line 1 cant be empty");

        RuleFor(x => x.addressLine2)
            .NotNull().WithMessage("The address line 2 cant be null")
            .NotEmpty().WithMessage("The address line 2 cant be empty");

        RuleFor(x => x.city)
            .NotNull().WithMessage("The city cant be null")
            .NotEmpty().WithMessage("The city cant be empty");

        RuleFor(x => x.population)
           .NotNull().WithMessage("The population cant be null")
           .GreaterThan(0).WithMessage("The population cant be zero or lower than zero");

        RuleFor(x => x.city)
            .NotNull().WithMessage("The city cant be null")
            .NotEmpty().WithMessage("The city cant be empty");

        RuleFor(x => x.postalCode)
           .NotNull().WithMessage("The postal code cant be null")
           .GreaterThan(0).WithMessage("The postal code cant be zero or lower than zero");

        RuleFor(x => x.city)
            .NotNull().WithMessage("The city cant be null")
            .NotEmpty().WithMessage("The city cant be empty");

        RuleFor(x => x.personNif)
            .NotNull().WithMessage("The person NIF cant be null")
            .NotEmpty().WithMessage("The person NIF cant be empty");
    }
}

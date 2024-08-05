using FluentValidation;

namespace Clinic.Business.PersonsAddress.Commands.DeletePersonAddress;

public class DeletePersonAddressCommandValidator : AbstractValidator<DeletePersonAddressCommand>
{
    public DeletePersonAddressCommandValidator()
    {
        RuleFor(x => x.addressId)
            .NotNull().WithMessage("The address id cant be null")
            .GreaterThan(0).WithMessage("The address id cant be zero or lower than zero");
    }
}

using Clinic.Business.PersonVacationPeriods.Commands.CreatePersonVacationPeriod;
using FluentValidation;

namespace Clinic.Business.PersonVacationPeriods.Validators;

public class CreatePersonVacationPeriodCommandValidator : AbstractValidator<CreatePersonVacationPeriodCommand>
{
    public CreatePersonVacationPeriodCommandValidator()
    {
        RuleFor(x => x.PersonId).NotEmpty().WithMessage("The PersonId is required.");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("The StartDate is required.");
        RuleFor(x => x.VacationPeriodStatusId).NotEmpty().WithMessage("The VacationPeriodStatusId is required.");
    }
}
using Clinic.Business.Patients.Commands.DeletePatient;
using FluentValidation;

namespace Clinic.Business.Patients.Validators
{
    public class DeletePatientValidator : AbstractValidator<DeletePatientCommand>
    {
        public DeletePatientValidator()
        {
            RuleFor(command => command.name)
                .NotEmpty().WithMessage("The Patient Name cant be empty.")
                .NotNull().WithMessage("The Patient Name cant be null");

            RuleFor(command => command.nif)
                .NotEmpty().WithMessage("The Patient Nif cant be empty.")
                .NotNull().WithMessage("The Patient Nif cant be null");
        }
    }
}

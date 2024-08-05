using Clinic.Business.Patients.Commands;
using FluentValidation;

namespace Clinic.Business.Patients.Validators
{
    public class UpdatePatientValidator : AbstractValidator<UpdatePatientCommand>
    {
        public UpdatePatientValidator()
        {
            RuleFor(command => command.nif)
                .NotEmpty().WithMessage("Patient Nif is required.");

            RuleFor(command => command.name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(command => command.telephone)
                .NotEmpty().WithMessage("Telephone is required.")
                .MaximumLength(20).WithMessage("Telephone must not exceed 20 characters.");

        }
    }
}

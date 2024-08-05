using Clinic.Business.Patients.Commands.CreatePatient;
using FluentValidation;

namespace Clinic.Business.Patients.Validators
{
    public class CreatePatientValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientValidator()
        {
            RuleFor(command => command.name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(command => command.telephone)
                .NotEmpty().WithMessage("Telephone is required.")
                .MaximumLength(20).WithMessage("Telephone must not exceed 20 characters.");

            RuleFor(command => command.nif)
                .NotEmpty().WithMessage("NIF is required.")
                .Length(9).WithMessage("NIF must be 9 characters long.");
            
        }
    }
}

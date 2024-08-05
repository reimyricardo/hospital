using FluentValidation;

namespace Clinic.Business.DoctorsPatient.Commands.CreateDoctorPatient;

public class CreateDoctorPatientCommandValidator : AbstractValidator<CreateDoctorPatientCommand>
{
    public CreateDoctorPatientCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotNull().WithMessage("The patient id cant be null")
            .GreaterThan(0).WithMessage("The patient id cant be zero or lower than zero");

        RuleFor(x => x.DoctorId)
            .NotNull().WithMessage("The doctor id cant be null")
            .GreaterThan(0).WithMessage("The doctor id cant be zero or lower than zero");
    }
}

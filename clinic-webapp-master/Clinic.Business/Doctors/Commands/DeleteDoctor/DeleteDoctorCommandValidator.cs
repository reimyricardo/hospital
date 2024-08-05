using FluentValidation;

namespace Clinic.Business.Doctors.Commands.DeleteDoctor;

public class DeleteDoctorCommandValidator: AbstractValidator<DeleteDoctorCommand>
{
    public DeleteDoctorCommandValidator() 
    { 
        RuleFor(x => x.doctorName)
            .NotEmpty()
            .WithMessage("The property {PropertyName} cant be empty");

        RuleFor(x => x.doctorName)
            .NotNull()
            .WithMessage("The property {PropertyName} cant be null");
    }

}

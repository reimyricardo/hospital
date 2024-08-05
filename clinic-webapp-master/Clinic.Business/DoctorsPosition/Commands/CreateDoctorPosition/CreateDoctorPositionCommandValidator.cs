using FluentValidation;

namespace Clinic.Business.DoctorsPosition.Commands.CreateDoctorPosition;

public class CreateDoctorPositionCommandValidator : AbstractValidator<CreateDoctorPositionCommand>
{
    public CreateDoctorPositionCommandValidator()
    {
        RuleFor(x => x.positionName).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(x => x.positionName).NotEmpty().WithMessage("The {PropertyName} cant be null");
    }
}

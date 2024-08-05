using FluentValidation;

namespace Clinic.Business.EmployeePositions.Commands.CreateEmployeePosition
{
    public class CreateEmployeePositionCommandValidator : AbstractValidator<CreateEmployeePositionCommand>
    {
        public CreateEmployeePositionCommandValidator()
        {
            RuleFor(x => x.positionName)
                .NotEmpty()
                .WithMessage("The {PropertyName} can't be empty");

            RuleFor(x => x.positionName)
                .NotNull()
                .WithMessage("The {PropertyName} can't be null");
        }
    }
}

using FluentValidation;

namespace Clinic.Business.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.employeeName)
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty");

            RuleFor(x => x.employeeName)
                .NotNull()
                .WithMessage("The property {PropertyName} cannot be null");
        }
    }
}


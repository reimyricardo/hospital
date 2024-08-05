using FluentValidation;
using System;
using System.Globalization;

namespace Clinic.Business.Employees.Commands.UpdateEmployee
{
    public class EmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public EmployeeCommandValidator()
        {
            RuleFor(c => c.employeeId)
                .GreaterThan(0)
                .WithMessage("The property {PropertyName} must be above zero.");

            RuleFor(c => c.name)
                .NotNull()
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty.");

            RuleFor(c => c.telephone)
                .NotNull()
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty.");

            RuleFor(c => c.startDate)
               .Must(startDate => DateTime.TryParse(startDate, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out DateTime _))
               .When(c => !string.IsNullOrEmpty(c.startDate))
               .WithMessage("The {PropertyName} is not a valid date");

            RuleFor(c => c.employeePosition)
                .NotNull()
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty.");
                
        }
    }
}

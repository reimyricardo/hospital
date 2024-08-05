using FluentValidation;
using System;
using System.Globalization;

namespace Clinic.Business.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.name).NotNull().WithMessage("The {PropertyName} cannot be null");
            RuleFor(x => x.name).NotEmpty().WithMessage("The {PropertyName} cannot be empty");

            RuleFor(x => x.startDate)
                .Must(startDate => DateTime.TryParse(startDate, DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out DateTime _))
                .When(x => !string.IsNullOrEmpty(x.startDate))
                .WithMessage("The {PropertyName} is not a valid date");
        }
    }
}


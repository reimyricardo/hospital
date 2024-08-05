using FluentValidation;
using System.Globalization;


namespace Clinic.Business.Doctors.Commands.UpdateDoctor
{
    public class DoctorCommandValidator : AbstractValidator<UpdateDoctorCommand>
    {
        public DoctorCommandValidator()
        {
            RuleFor(c => c.doctorName)
                .NotNull()
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty.");

            RuleFor(c => c.collegueNumber)
                .NotNull().WithMessage("The property {PropertyName} cant be null")
                .NotEmpty().WithMessage("The property {PropertyName} cant be empty");

            RuleFor(c => c.telephone)
                .NotNull()
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty.");

            RuleFor(c => c.startDate)
               .Must(startDate => DateTime.TryParse(startDate, DateTimeFormatInfo.CurrentInfo, out DateTime _))
               .When(c => !string.IsNullOrEmpty(c.startDate))
               .WithMessage("The {PropertyName} is not a valid date");

            RuleFor(c => c.doctorPosition)
                .NotNull()
                .NotEmpty()
                .WithMessage("The property {PropertyName} cannot be empty.");
        }
    }
}

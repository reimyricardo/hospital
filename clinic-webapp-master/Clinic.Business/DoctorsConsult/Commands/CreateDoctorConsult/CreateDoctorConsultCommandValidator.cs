using FluentValidation;
using System.Globalization;

namespace Clinic.Business.DoctorsConsult.Commands.CreateDoctorConsult;

public class CreateDoctorConsultCommandValidator : AbstractValidator<CreateDoctorConsultCommand>
{
    public CreateDoctorConsultCommandValidator()
    {
        RuleFor(x => x.doctorName)
            .NotEmpty()
            .WithMessage("The doctor name cant be empty");

        RuleFor(x => x.doctorCollegueNumber)
            .GreaterThan(0)
            .WithMessage("The doctor collegue number cant be lower than zero or equal to zero");

        RuleFor(x => x.consultDate)
            .NotEmpty()
            .WithMessage("The doctor consult date cant be empty");

        RuleFor(x => x.doctorName)
            .NotNull()
            .WithMessage("The doctor name cant be null");

        RuleFor(x => x.doctorCollegueNumber)
            .NotNull()
            .WithMessage("The doctor collegue number cant be null");

        RuleFor(x => x.consultDate)
            .NotNull()
            .WithMessage("The doctor consult date cant be null");

        RuleFor(x => x.consultDate)
            .Must(date => DateTime.TryParse(date, DateTimeFormatInfo.CurrentInfo, out DateTime _))
            .When(x => !string.IsNullOrEmpty(x.consultDate))
            .WithMessage("The doctor consult date is not a valid date");
    }
}

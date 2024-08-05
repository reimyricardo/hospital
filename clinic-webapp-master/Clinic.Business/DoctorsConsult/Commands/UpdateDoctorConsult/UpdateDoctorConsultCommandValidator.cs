using FluentValidation;
using System.Globalization;

namespace Clinic.Business.DoctorsConsult.Commands.UpdateDoctorConsult;

public class UpdateDoctorConsultCommandValidator : AbstractValidator<UpdateDoctorConsultCommand>
{
    public UpdateDoctorConsultCommandValidator()
    {
        RuleFor(x => x.consultId)
            .GreaterThan(0)
            .WithMessage("The consult id cant be lower than zero or equal to zero");

        RuleFor(x => x.doctorName)
            .NotEmpty()
            .WithMessage("The doctor name cant be empty");

        RuleFor(x => x.doctorName)
            .NotNull()
            .WithMessage("The doctor name cant be null");

        RuleFor(x => x.newConsultDate)
            .NotEmpty()
            .WithMessage("The new doctor consult date cant be empty");

        RuleFor(x => x.newConsultDate)
            .NotNull()
            .WithMessage("The new doctor consult date cant be null");

        RuleFor(x => x.newConsultDate)
            .Must(date => DateTime.TryParse(date, DateTimeFormatInfo.CurrentInfo, out DateTime _))
            .When(x => !string.IsNullOrEmpty(x.newConsultDate))
            .WithMessage("The new doctor consult date is not a valid date");
    }
}

using FluentValidation;
using System.Globalization;

namespace Clinic.Business.Doctors.Commands.CreateDoctor;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(x => x.name).NotNull().WithMessage("The {PropertyName} can be null");
        RuleFor(x => x.name).NotEmpty().WithMessage("The {PropertyName} can be empty");

        RuleFor(x => x.startDate)
               .Must(startDate => DateTime.TryParse(startDate,DateTimeFormatInfo.CurrentInfo,out DateTime _))
               .When(x => !string.IsNullOrEmpty(x.startDate))
               .WithMessage("The {PropertyName} is not a valid date");
    }
}

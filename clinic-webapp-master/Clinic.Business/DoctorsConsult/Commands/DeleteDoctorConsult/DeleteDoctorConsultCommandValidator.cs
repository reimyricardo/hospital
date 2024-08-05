using FluentValidation;

namespace Clinic.Business.DoctorsConsult.Commands.DeleteDoctorConsult;

public class DeleteDoctorConsultCommandValidator : AbstractValidator<DeleteDoctorConsultCommand>
{
    public DeleteDoctorConsultCommandValidator()
    {
        RuleFor(x => x.consultId)
            .GreaterThan(0)
            .WithMessage("The consult id cant be equal to zero or lower than zero");
    }
}

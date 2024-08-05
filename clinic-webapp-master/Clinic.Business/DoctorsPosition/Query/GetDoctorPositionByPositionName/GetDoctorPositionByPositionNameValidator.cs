using FluentValidation;

namespace Clinic.Business.DoctorsPosition.Query.GetDoctorPositionByPositionName;

public class GetDoctorPositionByPositionNameValidator : AbstractValidator<GetDoctorPositionByPositionNameQuery>
{
    public GetDoctorPositionByPositionNameValidator()
    {
        RuleFor(x => x.positionName).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(x => x.positionName).NotNull().WithMessage("The {PropertyName} cant be null");
    }
}

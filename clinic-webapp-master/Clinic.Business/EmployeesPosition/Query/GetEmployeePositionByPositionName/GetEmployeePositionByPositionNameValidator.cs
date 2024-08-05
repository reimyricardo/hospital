using FluentValidation;

namespace Clinic.Business.EmployeePositions.Query.GetEmployeePositionByPositionName;

public class GetEmployeePositionByPositionNameValidator : AbstractValidator<GetEmployeePositionByPositionNameQuery>
{
    public GetEmployeePositionByPositionNameValidator()
    {
        RuleFor(x => x.positionName).NotEmpty().WithMessage("The {PropertyName} cant be empty");

        RuleFor(x => x.positionName).NotNull().WithMessage("The {PropertyName} cant be null");
    }
}

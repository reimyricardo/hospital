using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Business.PersonVacationPeriods.Commands.UpdatePersonVacationPeriod
{
    public class UpdatePersonVacationPeriodCommandValidator : AbstractValidator<UpdatePersonVacationPeriodCommand>
    {
        public UpdatePersonVacationPeriodCommandValidator()
        {
            RuleFor(x => x.PersonVacationPeriodId).NotEmpty().WithMessage("The PersonVacationPeriodId is required.");
            RuleFor(x => x.StartDate).NotEmpty().WithMessage("The StartDate is required.");
            RuleFor(x => x.VacationPeriodStatusId).NotEmpty().WithMessage("The VacationPeriodStatusId is required.");
        }
    }
}

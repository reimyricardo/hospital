using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Business.PersonVacationPeriods.Commands.DeletePersonVacationPeriod
{
    public class DeletePersonVacationPeriodCommandValidator : AbstractValidator<DeletePersonVacationPeriodCommand>
    {
        public DeletePersonVacationPeriodCommandValidator()
        {
            RuleFor(x => x.PersonVacationPeriodId).NotEmpty().WithMessage("The PersonVacationPeriodId is required.");
        }
    }
}

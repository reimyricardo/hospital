using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.PersonVacationPeriods.Commands.UpdatePersonVacationPeriod
{
    public class PersonVacationError
    {
        public int PersonVacationPeriodId { get; }

        public PersonVacationError(int personVacationPeriodId)
        {
            PersonVacationPeriodId = personVacationPeriodId;
        }

        public static Error NotFoundById(int id)
        => Error.NotFound("PersonVacation.NotFound", $"The person vacation with the id of {id} was not found");
    }
}

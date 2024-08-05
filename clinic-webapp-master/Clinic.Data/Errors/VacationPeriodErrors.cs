using Clinic.Data.Entities.Common.Primitives;
using System;

namespace Clinic.Data.Errors
{
    public static class VacationPeriodErrors
    {
        public static Error NotFoundById(int id)
        => Error.NotFound("VacationPeriod.NotFound", $"The VacationPeriod with the id of {id} was not found");
    }
}

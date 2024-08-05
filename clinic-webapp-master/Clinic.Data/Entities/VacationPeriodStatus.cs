using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class VacationPeriodStatus : AuditableEntity
{
    public VacationPeriodStatus()
    {
        PersonVacationPeriods = new HashSet<PersonVacationPeriod>();
    }

    public string StatusName { get; set; } = string.Empty;

    public ICollection<PersonVacationPeriod> PersonVacationPeriods { get; set; }
}

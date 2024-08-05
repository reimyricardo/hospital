using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Data.DTOs;

public class PersonVacationPeriodResponse
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string PersonName { get; set; } = string.Empty;
    public string StartDate { get; set; } = string.Empty;
    public string? EndDate { get; set; }
    public int VacationPeriodStatusId { get; set; }
    public string VacationPeriodStatusName { get; set; } = string.Empty;
}

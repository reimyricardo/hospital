using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class DoctorConsult : AuditableEntity
{
    public int DoctorId { get; set; }

    public Doctor Doctor { get; set; } = new();

    public DateTime Date { get; set; }
}

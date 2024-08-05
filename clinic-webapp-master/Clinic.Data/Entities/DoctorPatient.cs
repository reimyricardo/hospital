using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class DoctorPatient : AuditableEntity
{
    public int? DoctorId { get; set; }

    public Doctor Doctor { get; set; } = new();

    public int? PatientId { get; set; }

    public Patient Patient { get; set; } = new();
}

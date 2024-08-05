using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class DoctorPosition : AuditableEntity
{
    public DoctorPosition()
    {
        Doctors = new HashSet<Doctor>();
    }

    public string PositionName { get; set; } = string.Empty;

    public ICollection<Doctor> Doctors { get; set; }
}

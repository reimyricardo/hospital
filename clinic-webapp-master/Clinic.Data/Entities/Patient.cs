using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class Patient : AuditableEntity
{
    public Patient()
    {
        DoctorPatients = new HashSet<DoctorPatient>();
    }

    public Person Person { get; set; } = new();

    public int PersonId { get; set; }

    public ICollection<DoctorPatient> DoctorPatients { get; set; } 
}

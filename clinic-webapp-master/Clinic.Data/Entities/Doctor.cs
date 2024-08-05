using Clinic.Data.Entities.Common;

namespace Clinic.Data.Entities;

public class Doctor : AuditableEntity
{
    public Doctor()
    {
        DoctorConsults = new HashSet<DoctorConsult>();
        DoctorPatients = new HashSet<DoctorPatient>();
    }

    public int CollegueNumber { get; set; } 

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; } = new DateTime(1999, 01, 01, 0, 0, 0, DateTimeKind.Unspecified);

    public Person Person { get; set; } = new(); // One to One

    public int PersonId { get; set; }

    public int DoctorPositionId { get; set; }

    public DoctorPosition DoctorPosition { get; set; } = new(); // One to One

    public ICollection<DoctorConsult> DoctorConsults { get; set; } // One to Many

    public ICollection<DoctorPatient> DoctorPatients { get; set; } // One to Many
}

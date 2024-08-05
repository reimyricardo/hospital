using Clinic.Data.Entities;

namespace Clinic.Data.Contracts;

public interface IDoctorPatientRepository
{
    Task<bool> IsDoctorAlreadyAssociatedWithThePatient(int doctorId,int patientId);

    void Add(DoctorPatient newDoctorPatient);
}

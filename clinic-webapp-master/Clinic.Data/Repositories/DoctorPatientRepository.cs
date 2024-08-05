using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data.Repositories;

public class DoctorPatientRepository : GenericRepository<DoctorPatient>, IDoctorPatientRepository
{
    public DoctorPatientRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsDoctorAlreadyAssociatedWithThePatient(int doctorId, int patientId)
    {
        return await _dbContext.DoctorPatient.AnyAsync(x => x.DoctorId == doctorId && x.PatientId == patientId);
    }
}

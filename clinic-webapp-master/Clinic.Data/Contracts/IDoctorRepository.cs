using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts;

public interface IDoctorRepository
{
    void Add(Doctor doctor);

    void Remove(Doctor doctor);

    void Update(Doctor doctor);

    Task<Doctor?> GetById(int doctorId,List<string>? includes = null);

    Task<bool> IsCollegueNumberNotAvaliable(int collegueNumber);

    Task<Doctor?> GetDoctorByNameAndCollegueNumber(string name,int collegueNumber);

    Task<Doctor?> GetDoctorPersonById(int id);

    Task<Doctor?> GetDoctorPersonByNameAndCollegueNumber(string name,int collegueNumber);

    Task<PagedList<DoctorResponse>> GetDoctorsInformation(string? name,string? sortColumn,string? sortOrder,int page,int pageSize);
}

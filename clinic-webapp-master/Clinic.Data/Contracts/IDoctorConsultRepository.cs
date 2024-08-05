using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts;

public interface IDoctorConsultRepository
{
    void Add(DoctorConsult newDoctorConsult);

    void Update(DoctorConsult doctorConsult);

    Task<DoctorConsult?> GetDoctorConsultByIdAndName(int consultId,string name);

    void Remove(DoctorConsult doctorConsult);

    Task<DoctorConsult?> GetById(int consultId, List<string>? includes = null);

    Task<PagedList<DoctorConsultResponse>> GetAllDoctorConsults(string? name, string? sortColumn, string? sortOrder, int page, int pageSize);
}

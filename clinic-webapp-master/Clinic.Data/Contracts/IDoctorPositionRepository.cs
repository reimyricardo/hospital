using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts;

public interface IDoctorPositionRepository
{
    Task AddDefaultDoctorPosition();

    Task<DoctorPosition?> GetDoctorPositionByPositionName(string positionName);

    Task<PagedList<DoctorPositionResponse>> GetAllDoctorPositions(string? name, string? sortColumn, string? sortOrder, int page, int pageSize);

    void Add(DoctorPosition doctorPosition);
}

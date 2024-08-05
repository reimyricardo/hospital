using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts
{
    public interface IEmployeePositionRepository
    {
        Task AddDefaultEmployeePosition();

        Task<EmployeePosition?> GetEmployeePositionByPositionName(string positionName);
      
        Task<PagedList<EmployeePositionResponse>> GetAllEmployeePositions(string? name, string? sortColumn, string? sortOrder, int page, int pageSize);

        void Add(EmployeePosition employeePosition);
    }
}



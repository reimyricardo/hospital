using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinic.Data.Contracts
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);

        void Remove(Employee employee);

        void Update(Employee employee);

        Task<Employee?> GetById(int employeeId, List<string>? includes = null);

        Task<bool> IsEmployeeNumberNotAvailable(int employeeNumber);

        Task<Employee?> GetEmployeeByNameAndNif(string name, string nif);

        Task<Employee?> GetEmployeePersonById(int id);

        Task<PagedList<EmployeeResponse>> GetEmployeesInformation(string? name, string? sortColumn, string? sortOrder, int page, int pageSize);
    }
}


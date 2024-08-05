using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories;

public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Employee?> GetEmployeeByNameAndNif(string name, string nif)
    {
        return await _dbContext.Employee.Include(x => x.Person)
                                         .Where(x => x.Person.Name == name && x.Person.NIF == nif)
                                         .FirstOrDefaultAsync();
    }

    public async Task<bool> IsEmployeeNumberNotAvailable(int employeeNumber)
    {
        return await _dbContext.Employee.AsNoTracking().AnyAsync(x => x.Id == employeeNumber);
    }

    public async Task<Employee?> GetEmployeePersonById(int id)
    {
        return await _dbContext.Employee.Include(x => x.Person).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<PagedList<EmployeeResponse>> GetEmployeesInformation(string? name, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        IQueryable<Employee> queryable = _dbContext.Employee
                                                    .Include(x => x.Person)
                                                    .Include(x => x.EmployeePosition);

        if (!string.IsNullOrWhiteSpace(name))
        {
            queryable = queryable.Where(x => x.Person.Name.Contains(name));
        }

        if (sortOrder?.ToLower() == "desc")
        {
            queryable = queryable.OrderByDescending(GetSortProperty(sortColumn));
        }

        if (sortOrder?.ToLower() == "asc")
        {
            queryable = queryable.OrderBy(GetSortProperty(sortColumn));
        }

        IQueryable<EmployeeResponse> employees = queryable
            .AsNoTracking()
            .Select(x =>
            new EmployeeResponse()
            {
                EmployeeId = x.Id,
                Name = x.Person.Name,
                Telephone = x.Person.Telephone,
                NIF = x.Person.NIF,
                SocialNumber = x.Person.SocialNumber,
                PositionName = x.EmployeePosition.PositionName
            });

        return await MakePagedList(employees, page, pageSize);
    }

    static Expression<Func<Employee, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "name" => employee => employee.Person.Name,
            "nif" => employee => employee.Person.NIF,
            "socialnumber" => employee => employee.Person.SocialNumber,
            "telephone" => employee => employee.Person.Telephone,
            _ => employee => employee.Id
        };
    }
}


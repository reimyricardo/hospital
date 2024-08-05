using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Business.Employees.Query.GetEmployeesInformation
{
    public record GetEmployeesInformationQuery(string? name,
                                               string? sortColumn,
                                               string? sortOrder,
                                               int page,
                                               int pageSize) : IQuery<Result<PagedList<EmployeeResponse>>>;

    public class GetEmployeesInformationQueryHandler : IQueryHandler<GetEmployeesInformationQuery, Result<PagedList<EmployeeResponse>>>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GetEmployeesInformationQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<PagedList<EmployeeResponse>>> Handle(GetEmployeesInformationQuery request, CancellationToken cancellationToken)
        {
            PagedList<EmployeeResponse> result = await _employeeRepository.GetEmployeesInformation(request.name, request.sortColumn, request.sortOrder, request.page, request.pageSize);

            return Result<PagedList<EmployeeResponse>>.Sucess(result);
        }
    }
}


using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.EmployeePositions.Query.GetAllEmployeePositions
{
    public record GetAllEmployeePositionsQuery(string? name,
                                                string? sortColumn,
                                                string? sortOrder,
                                                int page,
                                                int pageSize) : IQuery<Result<PagedList<EmployeePositionResponse>>>;

    public class GetAllEmployeePositionsQueryHandler : IQueryHandler<GetAllEmployeePositionsQuery, Result<PagedList<EmployeePositionResponse>>>
    {
        private readonly IEmployeePositionRepository _employeePositionRepository;

        public GetAllEmployeePositionsQueryHandler(IEmployeePositionRepository employeePositionRepository)
        {
            _employeePositionRepository = employeePositionRepository;
        }

        public async Task<Result<PagedList<EmployeePositionResponse>>> Handle(GetAllEmployeePositionsQuery request, CancellationToken cancellationToken)
        {
            PagedList<EmployeePositionResponse> result = await _employeePositionRepository.GetAllEmployeePositions(request.name, request.sortColumn, request.sortOrder, request.page, request.pageSize);

            return Result<PagedList<EmployeePositionResponse>>.Sucess(result);
        }
    }
}

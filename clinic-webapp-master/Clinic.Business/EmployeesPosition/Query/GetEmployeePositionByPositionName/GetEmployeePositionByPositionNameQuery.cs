using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.EmployeePositions.Query.GetEmployeePositionByPositionName
{
    public record GetEmployeePositionByPositionNameQuery(string positionName) : IQuery<Result<EmployeePosition>>;

    public class GetEmployeePositionByPositionNameQueryHandler : IQueryHandler<GetEmployeePositionByPositionNameQuery, Result<EmployeePosition>>
    {
        private readonly IEmployeePositionRepository _employeePositionRepository;

        public GetEmployeePositionByPositionNameQueryHandler(IEmployeePositionRepository employeePositionRepository)
        {
            _employeePositionRepository = employeePositionRepository;
        }

        public async Task<Result<EmployeePosition>> Handle(GetEmployeePositionByPositionNameQuery request, CancellationToken cancellationToken)
        {
            EmployeePosition? employeePosition = await _employeePositionRepository.GetEmployeePositionByPositionName(request.positionName);

            if (employeePosition is null)
            {
                return Result<EmployeePosition>.Failure(EmployeePositionErrors.NotFoundByPositionName(request.positionName));
            }

            return Result<EmployeePosition>.Sucess(employeePosition);
        }
    }
}


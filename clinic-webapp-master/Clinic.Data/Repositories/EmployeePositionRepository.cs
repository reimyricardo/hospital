using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories
{
    public class EmployeePositionRepository
        : GenericRepository<EmployeePosition>,
        IEmployeePositionRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeePositionRepository(AppDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddDefaultEmployeePosition()
        {
            ICollection<EmployeePosition> employeePositions = new HashSet<EmployeePosition>()
            {
                 new EmployeePosition() {PositionName = "Employee Title" },
                 new EmployeePosition() {PositionName = "Employee Intern" },
                 new EmployeePosition() {PositionName = "Substitute Employee"}
            };

            AddRange(employeePositions);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedList<EmployeePositionResponse>> GetAllEmployeePositions(string? name, string? sortColumn, string? sortOrder, int page, int pageSize)
        {
            IQueryable<EmployeePosition> positions = _dbContext.EmployeePosition;

            if (name is not null)
            {
                positions = positions.Where(x => x.PositionName.Contains(name));
            }

            if (sortOrder?.ToLower() == "desc")
            {
                positions = positions.OrderByDescending(GetSortProperty(sortColumn));
            }

            if (sortOrder?.ToLower() == "asc")
            {
                positions = positions.OrderBy(GetSortProperty(sortColumn));
            }

            IQueryable<EmployeePositionResponse> positionDTOs = positions.Select(x => new EmployeePositionResponse() { positionName = x.PositionName });

            return await MakePagedList(positionDTOs,page, pageSize);

            static Expression<Func<EmployeePosition, object>> GetSortProperty(string? sortColumn)
                => sortColumn?.ToLower() switch
                {
                    "name" => position => position.PositionName,
                    _ => position => position.Id
                };
        }

        public async Task<EmployeePosition?> GetEmployeePositionByPositionName(string positionName)
        {
            return await _dbContext.EmployeePosition
                                   .Where(position => position.PositionName == positionName)
                                   .AsNoTracking()
                                   .FirstOrDefaultAsync();
        }
    }
}

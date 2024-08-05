using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories;

public class DoctorPositionRepository 
    : GenericRepository<DoctorPosition>,
    IDoctorPositionRepository
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorPositionRepository(AppDbContext dbContext, IUnitOfWork unitOfWork) : base(dbContext)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddDefaultDoctorPosition()
    {
        ICollection<DoctorPosition> doctorPositions = new HashSet<DoctorPosition>()
        {
             new DoctorPosition() {PositionName = "Medico Titular" },
             new DoctorPosition() {PositionName = "Medico Interino" },
             new DoctorPosition() {PositionName = "Medico Sustituto"}
        };

        AddRange(doctorPositions);

        await _unitOfWork.SaveChangesAsync();
    }

    public Task<PagedList<DoctorPositionResponse>> GetAllDoctorPositions(string? name, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        IQueryable<DoctorPosition> positions = _dbContext.DoctorPosition;

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

        IQueryable<DoctorPositionResponse> positionDTOs = positions.Select(x => new DoctorPositionResponse() { positionName = x.PositionName });

        return MakePagedList(positionDTOs, page, pageSize);

        static Expression<Func<DoctorPosition, object>> GetSortProperty(string? sortColumn)
            => sortColumn?.ToLower() switch
            {
                "name" => position => position.PositionName,
                _ => position => position.Id
            };
    }

    public async Task<DoctorPosition?> GetDoctorPositionByPositionName(string positionName)
    {
        return await _dbContext.DoctorPosition
                               .Where(position => position.PositionName == positionName)
                               .AsNoTracking()
                               .FirstOrDefaultAsync();
    }
}

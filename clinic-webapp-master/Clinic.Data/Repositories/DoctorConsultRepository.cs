using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories;

public class DoctorConsultRepository : GenericRepository<DoctorConsult>, IDoctorConsultRepository
{
    public DoctorConsultRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<DoctorConsultResponse>> GetAllDoctorConsults(string? name, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        IQueryable<DoctorConsult> consults = _dbContext.DoctorConsult
                                            .Include(x => x.Doctor)
                                            .ThenInclude(x => x.Person);

        if (name is not null)
        {
            consults = consults.Where(x => x.Doctor.Person.Name == name);
        }

        if (sortOrder?.ToLower() == "asc")
        {
            consults = consults.OrderBy(GetSortProperty(sortColumn));
        }

        if (sortOrder?.ToLower() == "desc")
        {
            consults = consults.OrderByDescending(GetSortProperty(sortColumn));
        }

        IQueryable<DoctorConsultResponse> result = consults
            .AsNoTracking()
            .Select(x => new DoctorConsultResponse()
            {
            ConsultId = x.Id,
            DoctorName = x.Doctor.Person.Name,
            ConsultDate = x.Date.ToString("dd-MM-yyyy"),
            ConsultHour = x.Date.ToString("t")
        });


        return await MakePagedList(result,page,pageSize);

        static Expression<Func<DoctorConsult, object>> GetSortProperty(string? sortColumn)
           => sortColumn?.ToLower() switch
           {
               "date" => consult => consult.Date,
               _ => consult => consult.Id
           };
    }

    public async Task<DoctorConsult?> GetDoctorConsultByIdAndName(int consultId, string name)
    {
        return await _dbContext.DoctorConsult
            .Include(x => x.Doctor)
                .ThenInclude(x => x.Person)
            .FirstOrDefaultAsync(x => x.Id == consultId && x.Doctor.Person.Name == name);
    }
}

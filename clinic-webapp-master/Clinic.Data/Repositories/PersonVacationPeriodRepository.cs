using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories;

public class PersonVacationPeriodRepository : GenericRepository<PersonVacationPeriod>, IPersonVacationPeriodRepository
{
    public PersonVacationPeriodRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PersonVacationPeriod?> GetByIdWithDetailsAsync(int id)
    {
        return await _dbContext.Set<PersonVacationPeriod>()
            .Include(pvp => pvp.Person)
            .Include(pvp => pvp.VacationPeriodStatus)
            .FirstOrDefaultAsync(pvp => pvp.Id == id);
    }

    public async Task<List<PersonVacationPeriod>> GetByPersonIdAsync(int personId)
    {
        return await _dbContext.Set<PersonVacationPeriod>()
            .Include(pvp => pvp.Person)
            .Include(pvp => pvp.VacationPeriodStatus)
            .Where(pvp => pvp.PersonId == personId)
            .ToListAsync();
    }

    public async Task AddPersonVacationPeriodAsync(PersonVacationPeriod personVacationPeriod)
    {
        await _dbContext.Set<PersonVacationPeriod>().AddAsync(personVacationPeriod);
    }

    public void UpdatePersonVacationPeriod(PersonVacationPeriod personVacationPeriod)
    {
        _dbContext.Set<PersonVacationPeriod>().Update(personVacationPeriod);
    }

    public void DeletePersonVacationPeriod(PersonVacationPeriod personVacationPeriod)
    {
        _dbContext.Set<PersonVacationPeriod>().Remove(personVacationPeriod);
    }

    public async Task<PagedList<PersonVacationPeriodResponse>> GetPersonVacationPeriodsInformation(int personId, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        IQueryable<PersonVacationPeriod> queryable = _dbContext.Set<PersonVacationPeriod>()
            .Include(pvp => pvp.Person)
            .Include(pvp => pvp.VacationPeriodStatus)
            .Where(pvp => pvp.PersonId == personId);

        if (sortOrder?.ToLower() == "desc")
        {
            queryable = queryable.OrderByDescending(GetSortProperty(sortColumn));
        }
        else
        {
            queryable = queryable.OrderBy(GetSortProperty(sortColumn));
        }

        IQueryable<PersonVacationPeriodResponse> personVacationPeriods = queryable
            .AsNoTracking()
            .Select(pvp =>
                new PersonVacationPeriodResponse
                {
                    Id = pvp.Id,
                    PersonId = pvp.PersonId,
                    PersonName = pvp.Person.Name,
                    StartDate = pvp.StartDate.ToString("dd-MM-yyyy"),
                    EndDate = pvp.EndDate.HasValue ? pvp.EndDate.Value.ToString("dd-MM-yyyy") : null,
                    VacationPeriodStatusId = pvp.VacationPeriodStatusId,
                    VacationPeriodStatusName = pvp.VacationPeriodStatus.StatusName
                });

        return await MakePagedList(personVacationPeriods, page, pageSize);
    }

    private static Expression<Func<PersonVacationPeriod, object>> GetSortProperty(string? sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "startdate" => pvp => pvp.StartDate,
            "enddate" => pvp => pvp.EndDate ?? DateTime.MinValue,
            "vacationperiodstatusname" => pvp => pvp.VacationPeriodStatus.StatusName,
            _ => pvp => pvp.Id
        };
    }
}


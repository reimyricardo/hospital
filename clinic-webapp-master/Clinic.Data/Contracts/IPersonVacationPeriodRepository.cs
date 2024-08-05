using Clinic.Data.Common;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts;

public interface IPersonVacationPeriodRepository 
{
    Task<PersonVacationPeriod?> GetByIdWithDetailsAsync(int id);
    Task<List<PersonVacationPeriod>> GetByPersonIdAsync(int personId);
    Task AddPersonVacationPeriodAsync(PersonVacationPeriod personVacationPeriod);
    void UpdatePersonVacationPeriod(PersonVacationPeriod personVacationPeriod);
    void DeletePersonVacationPeriod(PersonVacationPeriod personVacationPeriod);
    Task<PagedList<PersonVacationPeriodResponse>> GetPersonVacationPeriodsInformation(int personId, string? sortColumn, string? sortOrder, int page, int pageSize);
}

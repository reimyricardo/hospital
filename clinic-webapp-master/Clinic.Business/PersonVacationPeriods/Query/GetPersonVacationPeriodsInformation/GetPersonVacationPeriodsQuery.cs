using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Business.PersonVacationPeriods.Query.GetPersonVacationPeriodsInformation
{
    public record GetPersonVacationPeriodsQuery(
        int PersonId, 
        string? SortColumn, 
        string? SortOrder, 
        int Page, 
        int PageSize) : IQuery<Result<PagedList<PersonVacationPeriodResponse>>>;

    public class GetPersonVacationPeriodsQueryHandler : IQueryHandler<GetPersonVacationPeriodsQuery, Result<PagedList<PersonVacationPeriodResponse>>>
    {
        private readonly IPersonVacationPeriodRepository _personVacationPeriodRepository;

        public GetPersonVacationPeriodsQueryHandler(IPersonVacationPeriodRepository personVacationPeriodRepository)
        {
            _personVacationPeriodRepository = personVacationPeriodRepository;
        }

        public async Task<Result<PagedList<PersonVacationPeriodResponse>>> Handle(GetPersonVacationPeriodsQuery request, CancellationToken cancellationToken)
        {
            PagedList<PersonVacationPeriodResponse> personVacationPeriods = await _personVacationPeriodRepository.GetPersonVacationPeriodsInformation(
                request.PersonId,
                request.SortColumn,
                request.SortOrder,
                request.Page,
                request.PageSize);

            return Result<PagedList<PersonVacationPeriodResponse>>.Sucess(personVacationPeriods);
        }
    }
}

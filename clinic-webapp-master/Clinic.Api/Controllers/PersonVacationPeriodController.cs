using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.PersonVacationPeriods.Commands.CreatePersonVacationPeriod;
using Clinic.Business.PersonVacationPeriods.Commands.DeletePersonVacationPeriod;
using Clinic.Business.PersonVacationPeriods.Commands.UpdatePersonVacationPeriod;
using Clinic.Business.PersonVacationPeriods.Query.GetPersonVacationPeriodsInformation;
using Clinic.Data.Common;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/personvacationperiods")]
[ApiController]
public class PersonVacationPeriodController : ControllerBase
{
    private readonly ISender _sender;

    public PersonVacationPeriodController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreatePersonVacationPeriod(CreatePersonVacationPeriodCommand createPersonVacationPeriod)
    {
        Result result = await _sender.Send(createPersonVacationPeriod);
        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdatePersonVacationPeriod(UpdatePersonVacationPeriodCommand updatePersonVacationPeriod)
    {
        Result result = await _sender.Send(updatePersonVacationPeriod);
        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpDelete("delete")]
    public async Task<ActionResult> DeletePersonVacationPeriod(int id)
    {
        Result result = await _sender.Send(new DeletePersonVacationPeriodCommand(id));
        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpGet("all")]
    public async Task<ActionResult<PagedList<PersonVacationPeriodResponse>>> GetPersonVacationPeriods(
        [FromQuery] int personId,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        Result<PagedList<PersonVacationPeriodResponse>> result = await _sender.Send(
            new GetPersonVacationPeriodsQuery(personId, sortColumn, sortOrder, page, pageSize));

        return result.IsSuccess ? Ok(result.Data) : result.ToProblemDetails();
    }
}
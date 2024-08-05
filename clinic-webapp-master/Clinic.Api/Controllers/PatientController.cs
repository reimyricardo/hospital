using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.Patients.Commands;
using Clinic.Business.Patients.Commands.CreatePatient;
using Clinic.Business.Patients.Commands.DeletePatient;
using Clinic.Business.Patients.Query.GetPatientInformation;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/patient")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly ISender _sender;

    public PatientController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreatePatient(CreatePatientCommand createPatient)
    {
        Result result = await _sender.Send(createPatient);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdatePatient(UpdatePatientCommand updatePatient)
    {
        Result result = await _sender.Send(updatePatient);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpDelete("remove")]
    public async Task<ActionResult> DeletePatient(DeletePatientCommand deletePatient)
    {
        Result result = await _sender.Send(deletePatient);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }
  

    [HttpGet("all")]
    public async Task<ActionResult<PagedList<PatientResponse>>> GetPatientInformation(string? name,
                                                                                 string? sortColumn,
                                                                                 string? sortOrder,
                                                                                 int page,
                                                                                 int pageSize)
    {
        Result<PagedList<PatientResponse>> result = await _sender.Send(new GetPatientInformationQuery(name, sortColumn, sortOrder, page, pageSize));

        return result.IsSuccess ? Ok(result.Data) : result.ToProblemDetails();
    }
}

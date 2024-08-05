using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.DoctorsPosition.Commands.CreateDoctorPosition;
using Clinic.Business.DoctorsPosition.Query.GetAllDoctorPositions;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/doctorposition")]
[ApiController]
public class DoctorPositionController : ControllerBase
{
    private readonly ISender _sender;

    public DoctorPositionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]

    public async Task<ActionResult> CreateDoctorPosition(CreateDoctorPositionCommand createDoctorPositionCommand)
    {
        Result result = await _sender.Send(createDoctorPositionCommand);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpGet("all")]

    public async Task<ActionResult<PagedList<DoctorPositionResponse>>> GetAllDoctorsPosition(string? name,
                                                                       string? sortColumn,
                                                                       string? sortOrder,
                                                                       int page,
                                                                       int pageSize)
    {
        Result<PagedList<DoctorPositionResponse>> result = await _sender.Send(new GetAllDoctorPositionsQuery(name, sortColumn, sortOrder, page, pageSize));

        return result.IsSuccess ? Ok(result.Data) : result.ToProblemDetails();
    }
}

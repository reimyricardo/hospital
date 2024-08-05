using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.EmployeePositions.Commands.CreateEmployeePosition;
using Clinic.Business.EmployeePositions.Query.GetAllEmployeePositions;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/employeeposition")]
    [ApiController]
    public class EmployeePositionController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeePositionController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateEmployeePosition(CreateEmployeePositionCommand createEmployeePositionCommand)
        {
            Result result = await _sender.Send(createEmployeePositionCommand);

            return result.IsSuccess ? NoContent() : result.ToProblemDetails();
        }

        [HttpGet("all")]
        public async Task<ActionResult<PagedList<EmployeePositionResponse>>> GetAllEmployeePositions(
            [FromQuery] string? name,
            [FromQuery] string? sortColumn,
            [FromQuery] string? sortOrder,
            [FromQuery] int page,
            [FromQuery] int pageSize)
        {
            Result<PagedList<EmployeePositionResponse>> result = await _sender.Send(
                    new GetAllEmployeePositionsQuery(name, sortColumn, sortOrder, page, pageSize));

            return result.IsSuccess ? Ok(result.Data) : result.ToProblemDetails();
        }

    }
}

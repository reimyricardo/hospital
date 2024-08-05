using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.Employees.Commands.CreateEmployee;
using Clinic.Business.Employees.Commands.DeleteEmployee;
using Clinic.Business.Employees.Commands.UpdateEmployee;
using Clinic.Business.Employees.Query.GetEmployeesInformation;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender _sender;

        public EmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateEmployee(CreateEmployeeCommand createEmployee)
        {
            Result result = await _sender.Send(createEmployee);
            return result.IsSuccess ? NoContent() : result.ToProblemDetails();
        }

        [HttpDelete("remove")]
        public async Task<ActionResult> DeleteEmployee(DeleteEmployeeCommand deleteEmployee)
        {
            Result result = await _sender.Send(deleteEmployee);
            return result.IsSuccess ? NoContent() : result.ToProblemDetails();
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeCommand updateEmployee)
        {
            Result result = await _sender.Send(updateEmployee);
            return result.IsSuccess ? NoContent() : result.ToProblemDetails();
        }

        [HttpGet("all")]
        public async Task<ActionResult<PagedList<EmployeeResponse>>> GetEmployeesInformation(string? name,
                                                                               string? sortColumn,
                                                                               string? sortOrder,
                                                                               int page,
                                                                               int pageSize)
        {
            Result<PagedList<EmployeeResponse>> result = await _sender.Send(new GetEmployeesInformationQuery(name, sortColumn, sortOrder, page, pageSize));
            return result.IsSuccess ? Ok(result.Data) : result.ToProblemDetails();
        }
    }
}


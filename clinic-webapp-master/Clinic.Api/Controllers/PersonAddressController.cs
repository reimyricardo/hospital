using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.PersonsAddress.Commands.CreatePersonAddress;
using Clinic.Business.PersonsAddress.Commands.DeletePersonAddress;
using Clinic.Business.PersonsAddress.Commands.UpdatePersonAddress;
using Clinic.Business.PersonsAddress.Query.GetAllPersonsAddress;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers;

[ApiVersion(1)]
[Route("api/v{v:apiVersion}/personaddress")]
[ApiController]
public class PersonAddressController : ControllerBase
{
    private readonly ISender _sender;

    public PersonAddressController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<ActionResult> CreatePersonAddress(CreatePersonAddressCommand createPersonAddressCommand)
    {
        Result result = await _sender.Send(createPersonAddressCommand);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpPut("update")]
    public async Task<ActionResult> UpdatePersonAddress(UpdatePersonAddressCommand updatePersonAddress)
    {
        Result result = await _sender.Send(updatePersonAddress);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpDelete("delete")]

    public async Task<ActionResult> RemovePersonAddress(DeletePersonAddressCommand deletePersonAddressCommand)
    {
        Result result = await _sender.Send(deletePersonAddressCommand);

        return result.IsSuccess ? NoContent() : result.ToProblemDetails();
    }

    [HttpGet("all")]

    public async Task<ActionResult<PagedList<PersonAddressResponse>>> GetAllPersonsAddress(string? name, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        Result<PagedList<PersonAddressResponse>> result = await _sender.Send(new GetAllPersonsAddressQuery(name, sortColumn,sortOrder,page,pageSize));

        return result.IsSuccess ? Ok(result.Data) : result.ToProblemDetails();  
    }
}

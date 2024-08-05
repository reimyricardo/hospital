using Asp.Versioning;
using Clinic.Api.Extensions;
using Clinic.Business.DoctorsPatient.Commands.CreateDoctorPatient;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/doctorpatient")]
    [ApiController]
    public class DoctorPatientController : ControllerBase
    {
        private readonly ISender _sender;

        public DoctorPatientController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateDoctorPatient(CreateDoctorPatientCommand createDoctorPatient)
        {
            Result result = await _sender.Send(createDoctorPatient);

            return result.IsSuccess ? NoContent() : result.ToProblemDetails();
        }
    }
}

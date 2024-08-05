using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;

namespace Clinic.Business.Patients.Queries.GetAllDoctorsFromPatient;

public record GetAllDoctorsFromPatientQuery(int PatientId) : IRequest<Result<List<DoctorResponse>>>;

public class GetAllDoctorsFromPatientQueryHandler : IRequestHandler<GetAllDoctorsFromPatientQuery, Result<List<DoctorResponse>>>
{
    private readonly IPatientRepository _patientRepository;

    public GetAllDoctorsFromPatientQueryHandler(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }

    public async Task<Result<List<DoctorResponse>>> Handle(GetAllDoctorsFromPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetById(request.PatientId);

        if (patient is null)
        {
            return Result<List<DoctorResponse>>.Failure(Error.NotFound("Patient.NotFound", "Patient not found"));
        }

        var doctors = await _patientRepository.GetAllDoctorsFromPatient(request.PatientId);

        return Result<List<DoctorResponse>>.Sucess(doctors!);
    }
}

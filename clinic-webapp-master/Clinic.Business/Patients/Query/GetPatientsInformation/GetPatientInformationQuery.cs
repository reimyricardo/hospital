using Clinic.Business.Doctors.Query.GetDoctorsInformation;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using MediatR;

namespace Clinic.Business.Patients.Query.GetPatientInformation
{
    public record GetPatientInformationQuery(string? name,
                                             string? sortColumn,
                                             string? sortOrder,
                                             int page,
                                             int pageSize) : IRequest<Result<PagedList<PatientResponse>>>;


    public class GetPatientsInformationQueryHandler : IRequestHandler<GetPatientInformationQuery, Result<PagedList<PatientResponse>>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsInformationQueryHandler(IPatientRepository patientrepository)
        {
            _patientRepository = patientrepository;
        }

        public async Task<Result<PagedList<PatientResponse>>> Handle(GetPatientInformationQuery request, CancellationToken cancellationToken)
        {
            PagedList<PatientResponse> result = await _patientRepository.GetPatientsInformation(request.name, request.sortColumn, request.sortOrder, request.page, request.pageSize);

            return Result<PagedList<PatientResponse>>.Sucess(result);
        }
    }



}

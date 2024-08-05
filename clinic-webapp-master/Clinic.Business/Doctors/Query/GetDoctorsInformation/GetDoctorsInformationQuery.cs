using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.Doctors.Query.GetDoctorsInformation;

public record GetDoctorsInformationQuery(string? name,
                                         string? sortColumn,
                                         string? sortOrder,
                                         int page,
                                         int pageSize) : IQuery<Result<PagedList<DoctorResponse>>>;

public class GetDoctorsInformationQueryHandler : IQueryHandler<GetDoctorsInformationQuery, Result<PagedList<DoctorResponse>>>
{
    private readonly IDoctorRepository _doctorRepository;

    public GetDoctorsInformationQueryHandler(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<Result<PagedList<DoctorResponse>>> Handle(GetDoctorsInformationQuery request, CancellationToken cancellationToken)
    {
        PagedList<DoctorResponse> result = await _doctorRepository.GetDoctorsInformation(request.name, request.sortColumn, request.sortOrder, request.page, request.pageSize);

        return Result<PagedList<DoctorResponse>>.Sucess(result);
    }
}

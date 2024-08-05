using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.DoctorsConsult.Query.GetAllDoctorConsults;

public record GetAllDoctorConsultsQuery(string? name,
                                        string? sortColumn,
                                        string? sortOrder,
                                        int page,
                                        int pageSize) : IQuery<Result<PagedList<DoctorConsultResponse>>>;

public class GetAllDoctorsConsultQueryHandler : IQueryHandler<GetAllDoctorConsultsQuery, Result<PagedList<DoctorConsultResponse>>>
{
    private readonly IDoctorConsultRepository _doctorConsultRepository;

    public GetAllDoctorsConsultQueryHandler(IDoctorConsultRepository doctorConsultRepository)
    {
        _doctorConsultRepository = doctorConsultRepository;
    }

    public async Task<Result<PagedList<DoctorConsultResponse>>> Handle(GetAllDoctorConsultsQuery request, CancellationToken cancellationToken)
    {
        PagedList<DoctorConsultResponse> result = await _doctorConsultRepository.GetAllDoctorConsults(request.name,
                                                                                              request.sortColumn,
                                                                                              request.sortOrder,
                                                                                              request.page,
                                                                                              request.pageSize);

        return Result<PagedList<DoctorConsultResponse>>.Sucess(result);
    }
}

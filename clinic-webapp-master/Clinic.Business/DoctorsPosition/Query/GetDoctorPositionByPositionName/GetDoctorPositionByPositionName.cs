using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.DoctorsPosition.Query.GetDoctorPositionByPositionName;

public record GetDoctorPositionByPositionNameQuery(string positionName) : IQuery<Result<DoctorPosition>>;

public class GetDoctorPositionByPositionNameQueryHandler : IQueryHandler<GetDoctorPositionByPositionNameQuery, Result<DoctorPosition>>
{
    private readonly IDoctorPositionRepository _doctorPositionRepository;

    public GetDoctorPositionByPositionNameQueryHandler(IDoctorPositionRepository doctorPositionRepository)
    {
        _doctorPositionRepository = doctorPositionRepository;
    }

    public async Task<Result<DoctorPosition>> Handle(GetDoctorPositionByPositionNameQuery request, CancellationToken cancellationToken)
    {
        DoctorPosition? doctorPosition = await _doctorPositionRepository.GetDoctorPositionByPositionName(request.positionName);

        if (doctorPosition is null)
        {
            return Result<DoctorPosition>.Failure(DoctorPositionErrors.NotFoundByPositionName(request.positionName));
        }

        return Result<DoctorPosition>.Sucess(doctorPosition);
    }
}

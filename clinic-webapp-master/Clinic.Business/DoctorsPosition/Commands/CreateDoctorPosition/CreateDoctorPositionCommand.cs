using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.DoctorsPosition.Commands.CreateDoctorPosition;

public record CreateDoctorPositionCommand(string positionName) : ICommand<Result> { }

public class CreateDoctorPositionCommandHandler : ICommandHandler<CreateDoctorPositionCommand, Result>
{
    private readonly IDoctorPositionRepository _doctorPositionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorPositionCommandHandler(IDoctorPositionRepository doctorPositionRepository,
                                              IUnitOfWork unitOfWork)
    {
        _doctorPositionRepository = doctorPositionRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(CreateDoctorPositionCommand request, CancellationToken cancellationToken)
    {
        DoctorPosition newDoctorPosition = new DoctorPosition()
        {
            PositionName = request.positionName
        };

        _doctorPositionRepository.Add(newDoctorPosition);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

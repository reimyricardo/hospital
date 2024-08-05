using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.DoctorsConsult.Commands.DeleteDoctorConsult;

public record DeleteDoctorConsultCommand(int consultId) : ICommand<Result>;

public class DeleteDoctorConsultCommandHandler : ICommandHandler<DeleteDoctorConsultCommand, Result>
{
    private readonly IDoctorConsultRepository _doctorConsultRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorConsultCommandHandler(IDoctorConsultRepository doctorConsultRepository,
                                             IUnitOfWork unitOfWork)
    {
        _doctorConsultRepository = doctorConsultRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(DeleteDoctorConsultCommand request, CancellationToken cancellationToken)
    {
        DoctorConsult? foundDoctorConsult = await _doctorConsultRepository.GetById(request.consultId);

        if (foundDoctorConsult is null)
        {
            return Result.Failure(DoctorConsultErrors.NotFound);
        }

        _doctorConsultRepository.Remove(foundDoctorConsult);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

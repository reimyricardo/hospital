using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.Doctors.Commands.DeleteDoctor;

public record DeleteDoctorCommand(string doctorName,
                                  int collegueNumber) : ICommand<Result>;

public class DeleteDoctorCommandHandler : ICommandHandler<DeleteDoctorCommand, Result>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
    {
        Doctor? foundDoctor = await _doctorRepository.GetDoctorByNameAndCollegueNumber(request.doctorName,request.collegueNumber);

        if (foundDoctor is null)
        {
            return Result.Failure(DoctorErrors.NotFoundByName(request.doctorName));
        }

        _doctorRepository.Remove(foundDoctor);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

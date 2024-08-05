using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.DoctorsConsult.Commands.UpdateDoctorConsult;

public record UpdateDoctorConsultCommand(int consultId,
                                         string doctorName,
                                         string newConsultDate) : ICommand<Result>;

public class UpdateDoctorConsultCommandHandler : ICommandHandler<UpdateDoctorConsultCommand, Result>
{
    private readonly IDoctorConsultRepository _doctorConsultRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDoctorConsultCommandHandler(IDoctorConsultRepository doctorConsultRepository,
                                             IUnitOfWork unitOfWork)
    {
        _doctorConsultRepository = doctorConsultRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateDoctorConsultCommand request, CancellationToken cancellationToken)
    {
        DoctorConsult? foundDoctorConsult = await _doctorConsultRepository.GetDoctorConsultByIdAndName(request.consultId,request.doctorName);

        if (foundDoctorConsult is null)
        {
            return Result.Failure(DoctorConsultErrors.NotFound);
        }

        foundDoctorConsult.Date = DateTime.Parse(request.newConsultDate);

        _doctorConsultRepository.Update(foundDoctorConsult);
        
        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

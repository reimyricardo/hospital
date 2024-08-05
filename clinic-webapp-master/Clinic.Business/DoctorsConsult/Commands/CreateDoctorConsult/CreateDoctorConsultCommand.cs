using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.DoctorsConsult.Commands.CreateDoctorConsult;

public record CreateDoctorConsultCommand(string doctorName,
                                         int doctorCollegueNumber,
                                         string consultDate) : ICommand<Result>;


public class CreateDoctorConsultCommandHandler : ICommandHandler<CreateDoctorConsultCommand, Result>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorConsultRepository _doctorConsultRepository;

    public CreateDoctorConsultCommandHandler(IDoctorRepository doctorRepository,
                                             IUnitOfWork unitOfWork,
                                             IDoctorConsultRepository doctorConsultRepository)
    {
        _doctorRepository = doctorRepository;
        _unitOfWork = unitOfWork;
        _doctorConsultRepository = doctorConsultRepository;
    }
    public async Task<Result> Handle(CreateDoctorConsultCommand request, CancellationToken cancellationToken)
    {
        Doctor? foundDoctor = await _doctorRepository.GetDoctorByNameAndCollegueNumber(request.doctorName, request.doctorCollegueNumber);

        if (foundDoctor is null)
        {
            return Result.Failure(DoctorErrors.NotFoundByNameAndCollegueNumber(request.doctorName, request.doctorCollegueNumber));
        }

        _unitOfWork.ChangeContextTrackerToUnchanged(foundDoctor);

        DoctorConsult doctorConsult = new()
        {
            Doctor = foundDoctor,
            Date = DateTime.Parse(request.consultDate)
        };

        _doctorConsultRepository.Add(doctorConsult);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

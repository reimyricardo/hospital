using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.DoctorsPatient.Commands.CreateDoctorPatient;

public record CreateDoctorPatientCommand(int PatientId, int DoctorId) : ICommand<Result>;

public class CreateDoctorPatientCommandHandler : ICommandHandler<CreateDoctorPatientCommand, Result>
{
    private readonly IDoctorPatientRepository _doctorPatientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDoctorPatientCommandHandler(IDoctorPatientRepository doctorPatientRepository,
                                            IDoctorRepository doctorRepository,
                                            IPatientRepository patientRepository,
                                            IUnitOfWork unitOfWork)
    {
        _doctorPatientRepository = doctorPatientRepository;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreateDoctorPatientCommand request, CancellationToken cancellationToken)
    {
        Patient? foundPatient = await _patientRepository.GetById(request.PatientId);

        Doctor? foundDoctor = await _doctorRepository.GetById(request.DoctorId);

        if (foundDoctor is null)
        {
            return Result.Failure(DoctorErrors.NotFoundById(request.DoctorId));
        }

        if (foundPatient is null)
        {
            return Result.Failure(PatientErrors.NotFoundById(request.PatientId));
        }

        if (await _doctorPatientRepository.IsDoctorAlreadyAssociatedWithThePatient(request.DoctorId, request.PatientId))
        {
            return Result.Failure(DoctorPatientErrors.AlreadyAssociatedPatient);
        }

        _unitOfWork.ChangeContextTrackerToUnchanged(foundDoctor);
        _unitOfWork.ChangeContextTrackerToUnchanged(foundPatient);


        DoctorPatient newDoctorPatient = new DoctorPatient()
        {
            Doctor = foundDoctor,
            Patient = foundPatient,
        };

        _doctorPatientRepository.Add(newDoctorPatient);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
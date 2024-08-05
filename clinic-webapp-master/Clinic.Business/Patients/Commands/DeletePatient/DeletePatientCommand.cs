using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.Patients.Commands.DeletePatient;

public record DeletePatientCommand(string name,string nif) : ICommand<Result>;

public class DeletePatientCommandHandler : ICommandHandler<DeletePatientCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;

    public DeletePatientCommandHandler(IUnitOfWork unitOfWork,
                                       IPatientRepository patientRepository)
    {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
    }

    public async Task<Result> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await _patientRepository.GetPatientByNameAndNif(request.name,request.nif);

        if (patient is null)
        {
            return Result.Failure(PatientErrors.NotFoundByNameAndNif(request.name,request.nif));
        }

        _patientRepository.Remove(patient);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

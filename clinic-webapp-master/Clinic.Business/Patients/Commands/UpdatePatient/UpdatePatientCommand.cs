using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.Patients.Commands;

public record UpdatePatientCommand(
    string name,
    string nif,
    string telephone) : ICommand<Result>;

public class UpdatePatientCommandHandler : ICommandHandler<UpdatePatientCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;

    public UpdatePatientCommandHandler(IUnitOfWork unitOfWork, IPatientRepository patientRepository)
    {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
    }

    public async Task<Result> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        Patient? patient = await _patientRepository.GetPatientByNameAndNif(request.name,request.nif);

        if (patient is null)
        {
            return Result.Failure(PatientErrors.NotFoundByNameAndNif(request.name,request.nif));
        }

        patient.Person.Name = request.name;
        patient.Person.Telephone = request.telephone;

        _patientRepository.Update(patient);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();

    }
}

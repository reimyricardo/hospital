using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.Patients.Commands.CreatePatient;

public record CreatePatientCommand(string name,
                                   string telephone,
                                   string nif,
                                   int socialNumber) : ICommand<Result>;

public class CreatePatientCommandHandler : ICommandHandler<CreatePatientCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPatientRepository _patientRepository;
    private readonly IPersonRepository _personRepository;

    public CreatePatientCommandHandler(IUnitOfWork unitOfWork,
                                       IPatientRepository patientRepository,
                                       IPersonRepository personRepository)
    {
        _unitOfWork = unitOfWork;
        _patientRepository = patientRepository;
        _personRepository = personRepository;
    }

    public async Task<Result> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        if (await _personRepository.IsNifNotAvaliable(request.nif))
        {
            return Result.Failure(PersonErrors.NifNotUnique);
        }

        if (await _personRepository.IsSocialNumberNotAvaliable(request.socialNumber))
        {
            return Result.Failure(PersonErrors.SocialNumberNotUnique);
        }

        Patient newPatient = new Patient()
        {
            Person = new Person()
            {
                Name = request.name,
                Telephone = request.telephone,
                NIF = request.nif,
                SocialNumber = request.socialNumber
            }
        };

        _patientRepository.Add(newPatient);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.Doctors.Commands.CreateDoctor;

public record CreateDoctorCommand(string name,
                                  string telephone,
                                  string nif,
                                  int socialNumber,
                                  int collegueNumber,
                                  string startDate,
                                  string doctorPosition) : ICommand<Result>;

public class CreateDoctorCommandHandler : ICommandHandler<CreateDoctorCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDoctorPositionRepository _doctorPosition;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPersonRepository _personRepository;

    public CreateDoctorCommandHandler(IUnitOfWork unitOfWork,
                                      IDoctorPositionRepository doctorPosition,
                                      IDoctorRepository doctorRepository,
                                      IPersonRepository personRepository)
    {
        _unitOfWork = unitOfWork;
        _doctorPosition = doctorPosition;
        _doctorRepository = doctorRepository;
        _personRepository = personRepository;
    }

    public async Task<Result> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        DoctorPosition? doctorPosition = await _doctorPosition.GetDoctorPositionByPositionName(request.doctorPosition);

        if (doctorPosition is null)
        {
            return Result.Failure(DoctorPositionErrors.NotFoundByPositionName(request.doctorPosition));
        }

        if (await _doctorRepository.IsCollegueNumberNotAvaliable(request.collegueNumber))
        {
            return Result.Failure(DoctorErrors.CollegueNumberNotUnique(request.collegueNumber));   
        }

        if (await _personRepository.IsNifNotAvaliable(request.nif))
        {
            return Result.Failure(PersonErrors.NifNotUnique);
        }

        if (await _personRepository.IsSocialNumberNotAvaliable(request.socialNumber))
        {
            return Result.Failure(PersonErrors.SocialNumberNotUnique);
        }

        _unitOfWork.ChangeContextTrackerToUnchanged(doctorPosition);

        Doctor newDoctor = new Doctor()
        {
            Person = new Person()
            {
                Name = request.name,
                Telephone = request.telephone,
                NIF = request.nif,
                SocialNumber = request.socialNumber,
            },
            CollegueNumber = request.collegueNumber,
            DoctorPosition = doctorPosition,
            StartDate = DateTime.Parse(request.startDate),
        };

        _doctorRepository.Add(newDoctor);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

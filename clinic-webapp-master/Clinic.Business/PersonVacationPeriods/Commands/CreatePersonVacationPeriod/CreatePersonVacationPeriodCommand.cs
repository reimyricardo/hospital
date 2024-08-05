using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;
using Clinic.Data.Repositories;

namespace Clinic.Business.PersonVacationPeriods.Commands.CreatePersonVacationPeriod;

public record CreatePersonVacationPeriodCommand(int PersonId, DateTime StartDate, DateTime? EndDate, int VacationPeriodStatusId) : ICommand<Result>;

public class CreatePersonVacationPeriodCommandHandler : ICommandHandler<CreatePersonVacationPeriodCommand, Result>
{
    private readonly IPersonRepository _personRepository;
    private readonly IVacationPeriodStatus _vacationPeriodStatusRepository;
    private readonly IPersonVacationPeriodRepository _personVacationPeriodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePersonVacationPeriodCommandHandler(IPersonRepository personRepository,
        IPersonVacationPeriodRepository personVacationPeriodRepository,IVacationPeriodStatus vacationPeriodStatusRepository,
        IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _vacationPeriodStatusRepository = vacationPeriodStatusRepository;
        _personVacationPeriodRepository = personVacationPeriodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(CreatePersonVacationPeriodCommand request, CancellationToken cancellationToken)
    {
        Person? person = await _personRepository.GetById(request.PersonId);
        if (person is null)
        {
            return Result.Failure(PersonErrors.NotFoundById(request.PersonId));
        }

        VacationPeriodStatus? vacationPeriodStatus = await _vacationPeriodStatusRepository.GetById(request.VacationPeriodStatusId);
        if (vacationPeriodStatus is null)
        {
            return Result.Failure(VacationPeriodErrors.NotFoundById(request.VacationPeriodStatusId));
        }

        _unitOfWork.ChangeContextTrackerToUnchanged(person);
        _unitOfWork.ChangeContextTrackerToUnchanged(vacationPeriodStatus);

        PersonVacationPeriod personVacationPeriod = new()
        {
            Person = person,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            VacationPeriodStatus = vacationPeriodStatus
        };

        await _personVacationPeriodRepository.AddPersonVacationPeriodAsync(personVacationPeriod);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
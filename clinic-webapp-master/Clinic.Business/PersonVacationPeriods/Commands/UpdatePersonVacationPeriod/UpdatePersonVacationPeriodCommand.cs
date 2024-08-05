using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;
using Clinic.Data.Repositories;

namespace Clinic.Business.PersonVacationPeriods.Commands.UpdatePersonVacationPeriod;

public record UpdatePersonVacationPeriodCommand(int PersonVacationPeriodId, DateTime StartDate, DateTime? EndDate, int VacationPeriodStatusId) : ICommand<Result>;

public class UpdatePersonVacationPeriodCommandHandler : ICommandHandler<UpdatePersonVacationPeriodCommand, Result>
{
    private readonly IPersonVacationPeriodRepository _personVacationPeriodRepository;
    private readonly IVacationPeriodStatus _vacationPeriodStatusRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonVacationPeriodCommandHandler(IPersonVacationPeriodRepository personVacationPeriodRepository,
        IVacationPeriodStatus vacationPeriodStatusRepository,
        IUnitOfWork unitOfWork)
    {
        _personVacationPeriodRepository = personVacationPeriodRepository;
        _vacationPeriodStatusRepository = vacationPeriodStatusRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdatePersonVacationPeriodCommand request, CancellationToken cancellationToken)
    {
        PersonVacationPeriod? personVacationPeriod = await _personVacationPeriodRepository.GetByIdWithDetailsAsync(request.PersonVacationPeriodId);
        if (personVacationPeriod is null)
        {
            return Result.Failure(PersonVacationError.NotFoundById(request.PersonVacationPeriodId));
        }

        VacationPeriodStatus? vacationPeriodStatus = await _vacationPeriodStatusRepository.GetById(request.VacationPeriodStatusId);
        if (vacationPeriodStatus is null)
        {
            return Result.Failure(VacationPeriodErrors.NotFoundById(request.VacationPeriodStatusId));
        }

        personVacationPeriod.StartDate = request.StartDate;
        personVacationPeriod.EndDate = request.EndDate;
        personVacationPeriod.VacationPeriodStatus = vacationPeriodStatus;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
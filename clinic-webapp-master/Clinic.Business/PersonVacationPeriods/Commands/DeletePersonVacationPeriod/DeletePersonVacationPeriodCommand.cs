using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;


namespace Clinic.Business.PersonVacationPeriods.Commands.DeletePersonVacationPeriod;

public record DeletePersonVacationPeriodCommand(int PersonVacationPeriodId) : ICommand<Result>;

public class DeletePersonVacationPeriodCommandHandler : ICommandHandler<DeletePersonVacationPeriodCommand, Result>
{
    private readonly IPersonVacationPeriodRepository _personVacationPeriodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonVacationPeriodCommandHandler(IPersonVacationPeriodRepository personVacationPeriodRepository,
        IUnitOfWork unitOfWork)
    {
        _personVacationPeriodRepository = personVacationPeriodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletePersonVacationPeriodCommand request, CancellationToken cancellationToken)
    {
        PersonVacationPeriod? personVacationPeriod = await _personVacationPeriodRepository.GetByIdWithDetailsAsync(request.PersonVacationPeriodId);
        if (personVacationPeriod is null)
        {
            return Result.Failure(Clinic.Data.Errors.VacationPeriodErrors.NotFoundById(request.PersonVacationPeriodId));
        }

        _personVacationPeriodRepository.DeletePersonVacationPeriod(personVacationPeriod);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
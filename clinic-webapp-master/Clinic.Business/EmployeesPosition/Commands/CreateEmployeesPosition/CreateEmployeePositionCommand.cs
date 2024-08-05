using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Business.EmployeePositions.Commands.CreateEmployeePosition;
public record CreateEmployeePositionCommand(string positionName): ICommand<Result> { }
public class CreateEmployeePositionCommandHandler : ICommandHandler<CreateEmployeePositionCommand, Result>
    {
        private readonly IEmployeePositionRepository _employeePositionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeePositionCommandHandler(IEmployeePositionRepository employeePositionRepository,
                                                    IUnitOfWork unitOfWork)
        {
            _employeePositionRepository = employeePositionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateEmployeePositionCommand request, CancellationToken cancellationToken)
        {
        EmployeePosition newEmployeePosition = new EmployeePosition()
        {
            PositionName = request.positionName
        };
        _employeePositionRepository.Add(newEmployeePosition);

        await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }





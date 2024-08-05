using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Business.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeCommand(
       int employeeId,
       string name,
       string telephone,
       string startDate,
       string employeePosition) : ICommand<Result>;

    public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeePositionRepository _employeePosition;
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeCommandHandler(IUnitOfWork unitOfWork,
                                            IEmployeePositionRepository employeePosition,
                                            IEmployeeRepository employeeRepository)
        {
            _unitOfWork = unitOfWork;
            _employeePosition = employeePosition;
            _employeeRepository = employeeRepository;
        }

        public async Task<Result> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            EmployeePosition? employeePosition = await _employeePosition.GetEmployeePositionByPositionName(request.employeePosition);

            if (employeePosition is null)
            {
                return Result.Failure(EmployeePositionErrors.NotFoundByPositionName(request.employeePosition));
            }

            _unitOfWork.ChangeContextTrackerToUnchanged(employeePosition);

            Employee? employee = await _employeeRepository.GetEmployeePersonById(request.employeeId);

            if (employee is null)
            {
                return Result.Failure(EmployeeErrors.NotFoundById(request.employeeId));
            }

            employee.Person.Name = request.name;
            employee.Person.Telephone = request.telephone;
            employee.EmployeePosition = employeePosition;

            _employeeRepository.Update(employee);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}


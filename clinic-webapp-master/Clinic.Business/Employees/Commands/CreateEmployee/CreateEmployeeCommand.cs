using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;
using Clinic.Data.Repositories;

namespace Clinic.Business.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(string name,
                                     string telephone,
                                     string nif,
                                     int socialNumber,
                                     string startDate,
                                     string employeePosition) : ICommand<Result>;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeePositionRepository _employeePositionRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPersonRepository _personRepository;

    public CreateEmployeeCommandHandler(IUnitOfWork unitOfWork,
                                        IEmployeePositionRepository employeePositionRepository,
                                        IEmployeeRepository employeeRepository,
                                        IPersonRepository personRepository)
    {
        _unitOfWork = unitOfWork;
        _employeePositionRepository = employeePositionRepository;
        _employeeRepository = employeeRepository;
        _personRepository = personRepository;
    }

    public async Task<Result> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        EmployeePosition? employeePosition = await _employeePositionRepository.GetEmployeePositionByPositionName(request.employeePosition);

        if (employeePosition is null)
        {
            return Result.Failure(EmployeePositionErrors.NotFoundByPositionName(request.employeePosition));
        }

        _unitOfWork.ChangeContextTrackerToUnchanged(employeePosition);

        if (await _personRepository.IsNifNotAvaliable(request.nif))
        {
            return Result.Failure(PersonErrors.NifNotUnique);
        }

        if (await _personRepository.IsSocialNumberNotAvaliable(request.socialNumber))
        {
            return Result.Failure(PersonErrors.SocialNumberNotUnique);
        }

        Employee newEmployee = new Employee()
        {
            Person = new Person()
            {
                Name = request.name,
                Telephone = request.telephone,
                NIF = request.nif,
                SocialNumber = request.socialNumber,
            },
            EmployeePosition = employeePosition,
        };

        _employeeRepository.Add(newEmployee);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}

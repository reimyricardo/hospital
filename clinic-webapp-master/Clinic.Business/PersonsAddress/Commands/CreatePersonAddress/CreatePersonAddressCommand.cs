using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.PersonsAddress.Commands.CreatePersonAddress;

public record CreatePersonAddressCommand(int streetNumber,
                                         string addressLine1,
                                         string addressLine2,
                                         string city,
                                         int population,
                                         int postalCode,
                                         string province,
                                         string personName,
                                         string personNif) : ICommand<Result>;

public class CreatePersonAddressCommandHandler : ICommandHandler<CreatePersonAddressCommand, Result>
{
    private readonly IPersonAddressRepository _personAddressRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;

    public CreatePersonAddressCommandHandler(IPersonAddressRepository personAddressRepository,
                                             IUnitOfWork unitOfWork,
                                             IPersonRepository personRepository)
    {
        _personAddressRepository = personAddressRepository;
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
    }

    public async Task<Result> Handle(CreatePersonAddressCommand request, CancellationToken cancellationToken)
    {
        Person? foundPerson = await _personRepository.GetPersonByNameAndNif(request.personName, request.personNif);

        if (foundPerson is null)
        {
            return Result.Failure(PersonErrors.NotFoundByNameAndNif(request.personName,request.personNif));
        }

        _unitOfWork.ChangeContextTrackerToUnchanged(foundPerson);

        PersonAddress personAddress = new PersonAddress()
        {
            StreerNumber = request.streetNumber,
            AddressLine1 = request.addressLine1,
            AddressLine2 = request.addressLine2,
            City = request.city,
            Population = request.population,
            PostalCode = request.postalCode,
            Province = request.province,
            Person = foundPerson,
        };

        _personAddressRepository.Add(personAddress);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

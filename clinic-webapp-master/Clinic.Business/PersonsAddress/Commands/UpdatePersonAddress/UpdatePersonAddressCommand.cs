using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.PersonsAddress.Commands.UpdatePersonAddress;

public record UpdatePersonAddressCommand(int addressId,
                                         int streetNumber,
                                         string addressLine1,
                                         string addressLine2,
                                         string city,
                                         int population,
                                         int postalCode,
                                         string province,
                                         string personNif) : ICommand<Result>;

public class UpdatePersonAddressCommandHandler : ICommandHandler<UpdatePersonAddressCommand, Result>
{
    private readonly IPersonAddressRepository _personAddressRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonAddressCommandHandler(IPersonAddressRepository personAddressRepository,
                                             IUnitOfWork unitOfWork)
    {
        _personAddressRepository = personAddressRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdatePersonAddressCommand request, CancellationToken cancellationToken)
    {
        PersonAddress? foundPersonAddress = await _personAddressRepository.GetPersonAddressByIdAndNIF(request.addressId,request.personNif);

        if (foundPersonAddress is null)
        {
            return Result.Failure(PersonAddressErrors.NotFoundByAddressIdAndNIF(request.addressId,request.personNif));
        }

        foundPersonAddress.AddressLine1 = request.addressLine1;
        foundPersonAddress.AddressLine2 = request.addressLine2;
        foundPersonAddress.StreerNumber = request.streetNumber;
        foundPersonAddress.City = request.city;
        foundPersonAddress.Population = request.population;
        foundPersonAddress.PostalCode = request.postalCode;
        foundPersonAddress.Province = request.province;

        _personAddressRepository.Update(foundPersonAddress);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

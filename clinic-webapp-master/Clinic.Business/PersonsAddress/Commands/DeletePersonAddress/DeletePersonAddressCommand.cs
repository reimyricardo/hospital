using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Errors;

namespace Clinic.Business.PersonsAddress.Commands.DeletePersonAddress;

public record DeletePersonAddressCommand(int addressId) : ICommand<Result>;

public class DeletePersonAddressCommandHandler : ICommandHandler<DeletePersonAddressCommand, Result>
{
    private readonly IPersonAddressRepository _personAddressRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePersonAddressCommandHandler(IPersonAddressRepository personAddressRepository,IUnitOfWork unitOfWork)
    {
        _personAddressRepository = personAddressRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeletePersonAddressCommand request, CancellationToken cancellationToken)
    {
        PersonAddress? foundPersonAddress = await _personAddressRepository.GetById(request.addressId);

        if (foundPersonAddress is null)
        {
            return Result.Failure(PersonAddressErrors.NotFoundById(request.addressId));
        }

        _personAddressRepository.Remove(foundPersonAddress);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities.Common.Primitives;
using System.Windows.Input;

namespace Clinic.Business.PersonsAddress.Query.GetAllPersonsAddress;

public record GetAllPersonsAddressQuery(string? name,
                                        string? sortColumn,
                                        string? sortOrder,
                                        int page,
                                        int pageSize) : IQuery<Result<PagedList<PersonAddressResponse>>>;

public class GetAllPersonsAddressQueryHandler : IQueryHandler<GetAllPersonsAddressQuery, Result<PagedList<PersonAddressResponse>>>
{
    private readonly IPersonAddressRepository _personAddressRepository;

    public GetAllPersonsAddressQueryHandler(IPersonAddressRepository personAddressRepository)
    {
        _personAddressRepository = personAddressRepository;
    }

    public async Task<Result<PagedList<PersonAddressResponse>>> Handle(GetAllPersonsAddressQuery request, CancellationToken cancellationToken)
    {
        PagedList<PersonAddressResponse> result = await _personAddressRepository.GetAllPersonsAddress(request.name,request.sortColumn,request.sortOrder,request.page,request.pageSize);

        return Result<PagedList<PersonAddressResponse>>.Sucess(result);
    }
}
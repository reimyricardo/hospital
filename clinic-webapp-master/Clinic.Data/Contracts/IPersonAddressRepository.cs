using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;

namespace Clinic.Data.Contracts;

public interface IPersonAddressRepository
{
    void Add(PersonAddress personAddress);

    void Update(PersonAddress personAddress);

    void Remove(PersonAddress personAddress);

    Task<PersonAddress?> GetById(int adddresId,List<string>? includes = null);

    Task<PersonAddress?> GetPersonAddressByIdAndNIF(int addressId,string nif);

    Task<PagedList<PersonAddressResponse>> GetAllPersonsAddress(string? personName,
                                                                string? sortColumn,
                                                                string? sortOrder,
                                                                int page,
                                                                int pageSize);
}

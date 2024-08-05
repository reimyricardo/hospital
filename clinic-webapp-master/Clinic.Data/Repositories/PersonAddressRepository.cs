using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories;

public class PersonAddressRepository : GenericRepository<PersonAddress>, IPersonAddressRepository
{
    public PersonAddressRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PagedList<PersonAddressResponse>> GetAllPersonsAddress(string? personName, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        IQueryable<PersonAddress> queryable = _dbContext.PersonAddress.Include(x => x.Person);

        if (!string.IsNullOrEmpty(personName))
        {
            queryable = queryable.Where(x => x.Person.Name == personName);
        }

        if (sortOrder?.ToLower() == "desc")
        {
            queryable = queryable.OrderByDescending(GetSortProperty(sortColumn));
        }

        if (sortOrder?.ToLower() == "asc")
        {
            queryable = queryable.OrderBy(GetSortProperty(sortColumn));
        }

        IQueryable<PersonAddressResponse> response = queryable
            .AsNoTracking()
            .Select(x => new PersonAddressResponse
        {
            AddressId = x.Id,
            StreerNumber = x.StreerNumber,
            AddressLine1 = x.AddressLine1,
            AddressLine2 = x.AddressLine2,
            City = x.City,
            PostalCode = x.PostalCode,
            Population = x.Population,
            Province = x.Province,
            PersonName = x.Province
        });

        return await MakePagedList(response,page,pageSize);

        static Expression<Func<PersonAddress, object>> GetSortProperty(string? sortColumn)
                => sortColumn?.ToLower() switch
                {
                    "name" => personAddress => personAddress.Person.Name,
                    "streetnumber" => personAddress => personAddress.StreerNumber,
                    "city" => personAddress =>  personAddress.City,
                    "population" => personAddress => personAddress.Population,
                    "province" => personAddress => personAddress.Province,
                    _ => personAddress => personAddress.Id
                };

    }

    public async Task<PersonAddress?> GetPersonAddressByIdAndNIF(int addressId, string nif)
    {
        return await _dbContext.PersonAddress.Include(x => x.Person)
                                             .AsNoTracking()
                                             .Where(x => x.Person.NIF == nif && x.Id == addressId)
                                             .FirstOrDefaultAsync();
    }
}

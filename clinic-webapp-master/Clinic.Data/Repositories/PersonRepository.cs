using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data.Repositories;

public class PersonRepository
    : GenericRepository<Person>,
    IPersonRepository
{
    public PersonRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Person?> GetPersonByNameAndNif(string name, string nif)
    {
        return await _dbContext.Person.Where(x => x.Name == name && x.NIF == nif).FirstOrDefaultAsync();
    }

    public async Task<Person?> GetById(int id)
    {
        return await _dbContext.Person.FindAsync(id);
    }


    public async Task<bool> IsNifNotAvaliable(string nif)
    {
        return await _dbContext.Person.AsNoTracking().AnyAsync(x => x.NIF == nif);
    }

    public async Task<bool> IsSocialNumberNotAvaliable(int socialNumber)
    {
        return await _dbContext.Person.AsNoTracking().AnyAsync(x => x.SocialNumber == socialNumber);
    }
}

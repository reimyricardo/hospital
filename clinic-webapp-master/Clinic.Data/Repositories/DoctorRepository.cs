using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.DTOs;
using Clinic.Data.Entities;
using Clinic.Data.Entities.Common.Primitives;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clinic.Data.Repositories;

public class DoctorRepository 
    : GenericRepository<Doctor>,
    IDoctorRepository
{
    public DoctorRepository(AppDbContext dbContext) : base(dbContext) { }

    public async Task<Doctor?> GetDoctorByNameAndCollegueNumber(string name, int collegueNumber)
    {
        return await _dbContext.Doctor.Include(x => x.Person)
                                      .Where(x => x.Person.Name == name && x.CollegueNumber == collegueNumber)
                                      .FirstOrDefaultAsync();
    }

    public async Task<bool> IsCollegueNumberNotAvaliable(int collegueNumber)
    {
        return await _dbContext.Doctor.AsNoTracking().AnyAsync(x => x.CollegueNumber == collegueNumber);
    }
    public async Task<Doctor?> GetDoctorPersonById(int id)
    {
        return await _dbContext.Doctor.Include(x => x.Person).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<PagedList<DoctorResponse>> GetDoctorsInformation(string? name, string? sortColumn, string? sortOrder, int page, int pageSize)
    {
        IQueryable<Doctor> queryable = _dbContext.Doctor
                                                .Include(x => x.Person)
                                                .Include(x => x.DoctorPosition);

        if (!string.IsNullOrWhiteSpace(name))
        {
            queryable = queryable.Where(x => x.Person.Name.Contains(name));
        }

        if (sortOrder?.ToLower() == "desc")
        {
            queryable = queryable.OrderByDescending(GetSortProperty(sortColumn));
        }

        if (sortOrder?.ToLower() == "asc")
        {
            queryable = queryable.OrderBy(GetSortProperty(sortColumn));
        }

        IQueryable<DoctorResponse> doctors = queryable
            .AsNoTracking()
            .Select(x =>
            new DoctorResponse()
            {
                DoctorId = x.Id,
                Name = x.Person.Name,
                Telephone = x.Person.Telephone,
                NIF = x.Person.NIF,
                SocialNumber = x.Person.SocialNumber,
                CollegueNumber = x.CollegueNumber,
                StartDate = x.StartDate.ToString("dd-MM-yyyy"), 
                EndDate = x.EndDate.ToString("dd-MM-yyyy"), 
                PositionName = x.DoctorPosition.PositionName
            });

        return await MakePagedList(doctors, page, pageSize);

        static Expression<Func<Doctor, object>> GetSortProperty(string? sortColumn)
            => sortColumn?.ToLower() switch
            {
                "colleguenumber" => doctor => doctor.CollegueNumber,
                "name" => doctor => doctor.Person.Name,
                "nif" => doctor => doctor.Person.NIF,
                "socialnumber" => doctor => doctor.Person.SocialNumber,
                "telephone" => doctor => doctor.Person.Telephone,
                _ => doctor => doctor.Id
            };
    }

    public async Task<Doctor?> GetDoctorPersonByNameAndCollegueNumber(string name, int collegueNumber)
    {
        return await _dbContext.Doctor.Include(x => x.Person).Where(x => x.Person.Name == name && x.CollegueNumber == collegueNumber).FirstOrDefaultAsync();
    }
}

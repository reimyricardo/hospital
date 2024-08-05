using Clinic.Data.Common;
using Clinic.Data.Contracts;
using Clinic.Data.Entities;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data.Repositories;

public class VacationPeriodStatusRepository : GenericRepository<VacationPeriodStatus>, IVacationPeriodStatus
{
    private readonly IUnitOfWork _unitOfWork;

    public VacationPeriodStatusRepository(AppDbContext dbContext,IUnitOfWork unitOfWork) : base(dbContext)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task AddDefaultVacationPeriodStatus()
    {
        ICollection<VacationPeriodStatus> vacationPeriods = new HashSet<VacationPeriodStatus>()
        {
            new VacationPeriodStatus(){StatusName = "Disfrutado" },
            new VacationPeriodStatus(){StatusName = "Planificado"}
        };

        AddRange(vacationPeriods);

        await _unitOfWork.SaveChangesAsync();   
    }

    public async Task<VacationPeriodStatus?> GetById(int id)
    {
        return await _dbContext.Set<VacationPeriodStatus>()
            .FirstOrDefaultAsync(vps => vps.Id == id);
    }

}

using Clinic.Data.Contracts;
using Clinic.Data.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Clinic.Data.Common;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public void ChangeContextTracker(object entity, EntityState entityState)
    {
        _appDbContext.Entry(entity).State = entityState;
    }

    public void ChangeContextTrackerToUnchanged(object entity)
    {
        _appDbContext.Entry(entity).State = EntityState.Unchanged;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _appDbContext.SaveChangesAsync(cancellationToken);
    }
}

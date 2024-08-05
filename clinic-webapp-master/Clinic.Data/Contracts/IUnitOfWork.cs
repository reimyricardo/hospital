using Microsoft.EntityFrameworkCore;

namespace Clinic.Data.Contracts;

public interface IUnitOfWork
{
    void ChangeContextTracker(object entity, EntityState entityState);

    void ChangeContextTrackerToUnchanged(object entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

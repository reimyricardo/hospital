using Clinic.Data.Contracts;
using Clinic.Data.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Clinic.Data.Persistence.Interceptors;

public class SoftDeleteEntitiesInterceptor : SaveChangesInterceptor   
{
    private readonly IDateService _dateService;

    public SoftDeleteEntitiesInterceptor(IDateService dateService)
    {
        _dateService = dateService;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        if (eventData.Context is not null)
        {
            UpdateSoftDeleteEntities(eventData.Context);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateSoftDeleteEntities(DbContext dbContext)
    {
        List<EntityEntry<SoftEntity>> entities = dbContext.ChangeTracker.Entries<SoftEntity>().Where(x => x.State == EntityState.Deleted).ToList();

        foreach (EntityEntry<SoftEntity> entity in entities)
        {
            entity.State = EntityState.Modified;

            SetCurrentPropertyValue(entity, nameof(SoftEntity.IsDeleted), true);
            SetCurrentPropertyValue(entity, nameof(SoftEntity.DeletedAt), _dateService.NowUTC);
        }

    }

     private static void SetCurrentPropertyValue(
         EntityEntry entityEntry,
         string propertyName,
         object property) 
               => entityEntry.Property(propertyName).CurrentValue = property; 
}

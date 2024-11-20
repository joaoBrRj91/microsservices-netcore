using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Ordering.Infrastructure.Data.Extensions;

namespace Ordering.Infrastructure.Data.Interceptors;

public sealed class AuditableEntityInterceptor(ILogger logger) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context is null)
        {
            logger.LogWarning("[{Soruce}] - Save Changes Interceptor Is Call With Context Null. Check This Issue",
                nameof(AuditableEntityInterceptor));

            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<IEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateBy = "JohnDev Created";
                entry.Entity.CreateAt = DateTime.UtcNow;

            }

            if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
            {
                entry.Entity.LastModifiedBy = "JohnDev Modified";
                entry.Entity.LastModified = DateTime.UtcNow;
            }
        }
    }
}

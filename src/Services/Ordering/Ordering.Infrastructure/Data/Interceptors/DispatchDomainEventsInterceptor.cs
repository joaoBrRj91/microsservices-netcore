using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Data.Interceptors;

public sealed class DispatchDomainEventsInterceptor(ILogger logger, IMediator mediator) : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
        return base.SavingChanges(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext? context)
    {
        if (context is null)
        {
            logger.LogWarning("[{Soruce}] - Save Changes Interceptor Is Call With Context Null. Check This Issue",
                nameof(AuditableEntityInterceptor));

            return;
        }

        var aggregates = FindAllAgreegates(context.ChangeTracker);

        var domainEvents = aggregates.SelectMany(a => a.DomainEvents);

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);

        ClearEventsDispatched(aggregates);

    }

    private static IEnumerable<IAggregate> FindAllAgreegates(ChangeTracker changeTracker)
    {
        return changeTracker
                 .Entries<IAggregate>()
                 .Where(e => e.Entity.DomainEvents.Any())
                 .Select(e => e.Entity);
    }

    private static void ClearEventsDispatched(IEnumerable<IAggregate> aggregates)
    {
        foreach (var agreegate in aggregates)
            agreegate.ClearDomainEvents();
    }
}

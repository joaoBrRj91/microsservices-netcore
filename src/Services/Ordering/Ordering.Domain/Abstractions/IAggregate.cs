namespace Ordering.Domain.Abstractions;

public interface IAggregate<TIdentity> : IEntity<TIdentity>
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    IDomainEvent[] ClearDomainEvents();
}

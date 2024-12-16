namespace Ordering.Domain.Events;

public record OrderDeleteEvent(Order Order) : IDomainEvent;
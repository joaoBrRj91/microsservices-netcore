namespace Ordering.Application.Orders.Events;

public sealed class OrderDeletedEventHandler(ILogger<OrderDeletedEventHandler> logger)
    : INotificationHandler<OrderDeleteEvent>
{
    public Task Handle(OrderDeleteEvent notification, CancellationToken cancellationToken)
    {
        var createNotification = notification as IDomainEvent;

        logger.LogInformation($"Order event {nameof(OrderDeleteEvent)} triggered by event type {createNotification.EventType} with event id {createNotification.EventId}");
        return Task.CompletedTask;
    }
}

namespace Ordering.Application.Orders.Events;

public sealed class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdateEvent>
{
    public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
    {
        var createNotification = notification as IDomainEvent;

        logger.LogInformation($"Order event {nameof(OrderUpdateEvent)} triggered by event type {createNotification.EventType} with event id {createNotification.EventId}");
        return Task.CompletedTask;
    }
}

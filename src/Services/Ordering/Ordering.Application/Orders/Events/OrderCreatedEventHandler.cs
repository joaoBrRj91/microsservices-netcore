namespace Ordering.Application.Orders.Events;

public sealed class OrderCreatedEventHandler(ILogger logger)
    : INotificationHandler<OrderCreateEvent>
{
    public Task Handle(OrderCreateEvent notification, CancellationToken cancellationToken)
    {
        var createNotification = notification as IDomainEvent;

        logger.LogInformation($"Order event {nameof(OrderCreateEvent)} triggered by event type {createNotification.EventType} with event id {createNotification.EventId}");
        return Task.CompletedTask;
    }
}

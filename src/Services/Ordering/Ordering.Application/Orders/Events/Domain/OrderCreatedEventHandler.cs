using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.Events;

public sealed class OrderCreatedEventHandler(
    IPublishEndpoint publishEndpoint,IFeatureManager featureManager,
    ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreateEvent>
{
    public async Task Handle(OrderCreateEvent domainEvent, CancellationToken cancellationToken)
    {
        var createNotification = domainEvent as IDomainEvent;

        logger.LogInformation($"Order event {nameof(OrderCreateEvent)} triggered by event type {createNotification.EventType} with event id {createNotification.EventId}");

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            //var orderCreatedIntegrationEvent = domainEvent.Order.BuildOrderCreatedIntegrationEvent();
            await publishEndpoint.Publish(new { Event = "order-created-integration-event" }, cancellationToken);
        }
    }
}

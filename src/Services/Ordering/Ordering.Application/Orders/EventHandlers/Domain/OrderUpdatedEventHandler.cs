namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderUpdatedEventHandler
        (ILogger<OrderUpdatedEventHandler> logger)
        : INotificationHandler<OrderUpdateEvent>
    {
        public Task Handle(OrderUpdateEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType);
            return Task.CompletedTask;
        }
    }
}
using Ordering.Application.Builders.CreateOrder;

namespace Ordering.Application.Orders.Commands;

public sealed class CreateOrderHandler(IAppDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = command.Order.BuildOrder();

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }
}

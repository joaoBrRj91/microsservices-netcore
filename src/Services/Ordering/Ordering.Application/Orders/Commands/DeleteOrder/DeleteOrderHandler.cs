using Ordering.Application.Exceptions;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderHandler(IAppDbContext dbContext)
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FindAsync([OrderId.Of(command.OrderId)], cancellationToken)
            ?? throw new OrderNotFoundException(command.OrderId);

        order.Delete();

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}

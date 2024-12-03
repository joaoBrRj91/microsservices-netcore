
using Ordering.Application.Dtos;
using Ordering.Application.Exceptions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Application.Orders.Commands.UpdateOrder;
public class UpdateOrderHandler(IAppDbContext dbContext)
    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders.FindAsync([OrderId.Of(command.Order.Id)], cancellationToken) 
            ?? throw new OrderNotFoundException(command.Order.Id);

        command.Order.BuildUpdateOrder(order);

        CheckUpdatedOrderItems(order, command.Order.OrderItems);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private static void CheckUpdatedOrderItems(Order order, IEnumerable<OrderItemDto> orderItemsDto)
    {
        foreach (var orderItemDto in orderItemsDto)
            order.UpdateOrderItem(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
    }
}
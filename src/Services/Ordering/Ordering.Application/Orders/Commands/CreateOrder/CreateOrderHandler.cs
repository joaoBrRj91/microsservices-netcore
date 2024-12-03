using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderHandler(IAppDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = command.Order.BuildCreateOrder();
        AddOrderItems(order, command.Order.OrderItems);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private static void AddOrderItems(Order order, IEnumerable<OrderItemDto> orderItemsDto)
    {
        foreach (var orderItemDto in orderItemsDto)
            order.AddOrderItem(ProductId.Of(orderItemDto.ProductId), orderItemDto.Quantity, orderItemDto.Price);
    }
}

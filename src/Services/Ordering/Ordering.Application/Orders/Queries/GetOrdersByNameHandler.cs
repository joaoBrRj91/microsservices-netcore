using Microsoft.EntityFrameworkCore;
using Ordering.Application.Dtos;
using Ordering.Domain.Models;

namespace Ordering.Application.Orders.Queries
{
    internal class GetOrdersByNameHandler(IAppDbContext appDbContext) 
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders =  appDbContext
                .Orders
                .Where(o => o.OrderName.Value == request.Name)
                .OrderBy(o => o.OrderName)
                .AsAsyncEnumerable();

            return await GetOrdersByNameDto(orders);
        }

        private async Task<GetOrdersByNameResult> GetOrdersByNameDto(IAsyncEnumerable<Order> orders)
        {
            var ordersDto = new List<OrderDto>();

            await foreach (var order in orders) 
            {
                ordersDto.Add(order.BuildOrderDto());
            }

            return new GetOrdersByNameResult(ordersDto);
        }
    }
}

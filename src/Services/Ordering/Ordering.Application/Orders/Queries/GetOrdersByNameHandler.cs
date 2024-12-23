using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Orders.Queries
{
    internal class GetOrdersByNameHandler(IAppDbContext appDbContext) 
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders =  await appDbContext
                .Orders
                .AsNoTracking()
                .Where(o => o.OrderName.Value == request.Name)
                .OrderBy(o => o.OrderName)
                .ToListAsync();

            return new GetOrdersByNameResult(orders.BuildOrdersDto());
        }
    }
}

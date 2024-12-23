using Microsoft.EntityFrameworkCore;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

internal class GetOrdersByCustomerHandler(IAppDbContext appDbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await appDbContext
            .Orders
            .AsNoTracking()
            .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
            .OrderBy(o => o.OrderName)
            .ToListAsync(cancellationToken);

        return new GetOrdersByCustomerResult(orders.BuildOrdersDto());
    }
}

using BuildingBlocks.Core.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Queries.GetOrders;

internal class GetOrdersHandler(IAppDbContext appDbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var pageIndex = query.PaginationRequest.PageIndex;
        var pageSize = query.PaginationRequest.PageSize;

        var totalOrders = await appDbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await appDbContext
                .Orders
                .AsNoTracking()
                .OrderBy(o => o.OrderName.Value)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);


        return new GetOrdersResult(new PaginatedResult<OrderDto>
            (pageIndex,
            pageSize,
            totalOrders,
            orders.BuildOrdersDto()));
    }
}

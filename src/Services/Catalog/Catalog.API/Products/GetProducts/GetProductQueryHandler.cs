using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProducts;

public record GetProductQuery() : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal sealed class GetProductQueryHandler(
    ILogger<GetProductQueryHandler> logger, IDocumentSession session)
    : IQueryHandler<GetProductQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("{GetProductQueryHandler} called with {query}", nameof(GetProductQueryHandler), query);

        var products = await session.Query<Product>().ToListAsync(cancellationToken);

        return new GetProductsResult(products);
    }
}

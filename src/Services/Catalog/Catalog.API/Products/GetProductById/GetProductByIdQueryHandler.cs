using Catalog.API.Products.GetProducts;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal sealed class GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger,
    IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("{GetProductByIdQueryHandler} called with {query}", nameof(GetProductByIdQueryHandler), query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if(product is null)
            throw new ProductNotFoundException();
    }
}

//TODO : Representation Application logic layer in vertical slice archictecture
using BuildingBlocks.Core.CQRS;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand
    (string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;


public record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business loigc to create a product
        throw new NotImplementedException();
    }
}




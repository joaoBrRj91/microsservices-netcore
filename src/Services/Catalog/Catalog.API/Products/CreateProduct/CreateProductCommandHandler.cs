//TODO : Representation Application logic layer in vertical slice archictecture
using BuildingBlocks.Core.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct;


#region Command
public record CreateProductCommand
    (string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);
#endregion

#region Handler
internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Business logic to create a product

        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        //Save database (Transaction DB Marten)

        return new CreateProductResult(Guid.NewGuid());
    }
}
#endregion




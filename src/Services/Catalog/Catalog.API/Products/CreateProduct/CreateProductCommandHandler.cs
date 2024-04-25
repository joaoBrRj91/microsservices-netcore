//TODO : Representation Application logic layer in vertical slice archictecture
using BuildingBlocks.Core.CQRS;
using MediatR;

namespace Catalog.API.Products.CreateProduct;

public record CreateProductCommand
    (string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : IRequest<CreateProductResult>;


public record TestCommand(string Teste) : ICommand;
public record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        //Business loigc to create a product
        throw new NotImplementedException();
    }
}




using Catalog.API.Exceptions;

namespace Catalog.API.Products.DeleteProduct;

#region Command
public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
public record DeleteProductResult(bool IsSuccess);
#endregion

#region Validator
public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator() 
        => RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required");
}
#endregion

#region Handler
internal sealed class DeleteProductCommandHandler(ILogger<DeleteProductCommandHandler> logger,
    IDocumentSession session)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("{DeleteProductCommandHandler} called with {command}", nameof(DeleteProductCommandHandler), command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException();

        session.Delete(product);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
#endregion

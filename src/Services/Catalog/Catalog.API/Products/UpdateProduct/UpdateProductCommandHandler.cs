﻿//TODO : Representation Application logic layer in vertical slice archictecture
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;

#region Command
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
: ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);
#endregion

#region Validator
public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Product Id is required");

        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Product Id is required")
            .Length(2, 150)
            .WithMessage("Name must be between 2 and 150 characters");

        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0");

    }
}
#endregion

#region Handler
internal sealed class UpdateProductCommandHandler(
    ILogger<UpdateProductCommandHandler> logger,
    IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("{UpdateProductCommandHandler} called with {command}", nameof(UpdateProductCommandHandler), command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ?? throw new ProductNotFoundException(command.Id);
        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
#endregion

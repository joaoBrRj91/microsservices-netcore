using BuildingBlocks.Core.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands;

public record CreateOrderCommand(OrderDto Order) : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(o => o.Order.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(o => o.Order.CustomerId).NotEqual(default(Guid)).WithMessage("CustomerId is invalid");
        RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}
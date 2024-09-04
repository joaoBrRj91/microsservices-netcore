using Discount.Grpc;

namespace Basket.API.Baskets.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
public record StoreBasketResult(bool IsSuccess, string UserName);


public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(b => b.Cart).NotEmpty().WithMessage("Cart can not be empty");
        RuleFor(b => b.Cart.UserName).NotEmpty().WithMessage("UserName is required");
    }
}

public class StoreBasketCommandHandler(
    IBasketRepository basketRepository,
    DiscountProtoService.DiscountProtoServiceClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
     
        await DeductDiscount(command.Cart, cancellationToken);

        await basketRepository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(true, command.Cart.UserName);
    }

    private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
    {
        foreach (var item in cart.Items)
        {
            var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName },cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }

    }
}

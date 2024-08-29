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
        //TODO : Communicate with Discount.Grpc and calculate lastest prices of products into itens in cart
        //TODO : Debt Tech - Create an grpc method for calculate discounts of a list of products
        await DeductDiscount(discountClient, command);

        //TODO : Store basket in database (use Marten upsert - if exist = update, if not create)
        //TODO : Update Cache service with changes basket user 
        await basketRepository.StoreBasket(command.Cart, cancellationToken);

        return new StoreBasketResult(true, command.Cart.UserName);
    }

    private static async Task DeductDiscount(DiscountProtoService.DiscountProtoServiceClient discountClient, StoreBasketCommand command)
    {
        foreach (var item in command.Cart.Items)
        {
            var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });
            item.Price -= coupon.Amount;
        }

    }
}

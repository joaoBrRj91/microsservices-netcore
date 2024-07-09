
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

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {

        var cart = request.Cart;

        //TODO : Store basket in database (use Marten upsert - if exist = update, if not create)
        //TODO : Update Cache service with changes basket user 

        return await Task.FromResult(new StoreBasketResult(true, cart.UserName));
    }
}

using Discount.Grpc;
using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Services;

internal class DiscountService
    (DiscountContext discountContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext
            .Coupons
            .FirstOrDefaultAsync(d => d.ProductName == request.ProductName);

        if (coupon is null)
            coupon = new Coupon { ProductName = request.ProductName, Descriptiom = "No Discount", Amount = 0 };

        logger.LogInformation("Discount is retrieved for product : {productName}, amount: {amount}, description: {description}", coupon.ProductName, coupon.Amount, coupon.Descriptiom);

        return new CouponModel { ProductName = request.ProductName, Amount = coupon.Amount };
    }

    public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        return base.CreateDiscount(request, context);

    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }
}

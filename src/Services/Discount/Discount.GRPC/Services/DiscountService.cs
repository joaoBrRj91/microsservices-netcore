using Discount.Grpc;
using Discount.GRPC.Data;
using Discount.GRPC.Models;
using Grpc.Core;
using Mapster;
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

        coupon ??= new Coupon { ProductName = request.ProductName, Description = "No Discount", Amount = 0 };

        logger.LogInformation("Discount is retrieved for product : {productName}, amount: {amount}, description: {description}", coupon.ProductName, coupon.Amount, coupon.Description);

        return BuildCouponModelByCoupon(coupon);
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        discountContext.Coupons.Add(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation("Discount is created for product : {productName}, amount: {amount}, description: {description}", coupon.ProductName, coupon.Amount, coupon.Description);

        return BuildCouponModelByCoupon(coupon);

    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }


    private static CouponModel BuildCouponModelByCoupon(Coupon coupon) 
        => new() { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };

}
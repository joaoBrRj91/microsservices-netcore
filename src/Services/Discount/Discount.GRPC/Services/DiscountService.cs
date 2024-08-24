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

    private const string BASE_INFO_LOG_SUCCESS = "Discount is {operation} for product : {productName}, amount: {amount}, description: {description}";

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await FindCouponByProductName(request.ProductName);

        coupon ??= new Coupon { ProductName = request.ProductName, Description = "No Discount", Amount = 0 };

        logger.LogInformation(BASE_INFO_LOG_SUCCESS, "retrived", coupon.ProductName, coupon.Amount, coupon.Description);

        return BuildCouponModelByCoupon(coupon);
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();

        ThrowRpcExceptionIfInvalidCoupon(coupon, StatusCode.InvalidArgument, "Invalid Request");

        discountContext.Coupons.Add(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation(BASE_INFO_LOG_SUCCESS, "created", coupon.ProductName, coupon.Amount, coupon.Description);

        return BuildCouponModelByCoupon(coupon);

    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = await discountContext
            .Coupons
            .FindAsync(request.Coupon.Id);

        ThrowRpcExceptionIfInvalidCoupon(coupon, StatusCode.NotFound, "Not Found Coupon");

        coupon = request.Coupon.Adapt(coupon)!;

        discountContext.Coupons.Update(coupon);
        await discountContext.SaveChangesAsync();

        logger.LogInformation(BASE_INFO_LOG_SUCCESS, "updated", coupon.ProductName, coupon.Amount, coupon.Description);

        return BuildCouponModelByCoupon(coupon);
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await FindCouponByProductName(request.ProductName);

        ThrowRpcExceptionIfInvalidCoupon(coupon, StatusCode.NotFound, "Not Found Coupon");

        discountContext.Coupons.Remove(coupon!);
        await discountContext.SaveChangesAsync();

        logger.LogInformation(BASE_INFO_LOG_SUCCESS, "deleted", coupon!.ProductName, coupon.Amount, coupon.Description);

        return new DeleteDiscountResponse { Success = true };
    }


    private async Task<Coupon?> FindCouponByProductName(string productName) 
        => await discountContext
           .Coupons
           .FirstOrDefaultAsync(d => d.ProductName == productName);

    private static CouponModel BuildCouponModelByCoupon(Coupon coupon)
        => new() { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount };

    private static void ThrowRpcExceptionIfInvalidCoupon(Coupon? coupon, StatusCode status, string message = "Not informed")
    {
        if (coupon is null)
            throw new RpcException(new Status(status, message));
    }

}
namespace Discount.GRPC.Models;

internal class Coupon
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Descriptiom { get; set; } = default!;
    public int Amount { get; set; } = default!;

}

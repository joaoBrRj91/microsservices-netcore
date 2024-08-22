using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

internal class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
}

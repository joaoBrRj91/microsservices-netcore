using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

internal class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        //Seed when call Add-Migration
        modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id= Guid.NewGuid(), ProductName = "IPhone 15", Descriptiom = "IPhone Description", Amount= 499 },
                new Coupon { Id = Guid.NewGuid(), ProductName = "IPhone 15", Descriptiom = "IPhone Description", Amount = 499 }
            );
    }
}

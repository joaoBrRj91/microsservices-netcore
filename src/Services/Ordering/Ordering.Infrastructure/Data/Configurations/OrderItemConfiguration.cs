using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);

        builder.Property(oi => oi.Id)
               .HasConversion(
                orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId));

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(oi => oi.ProductId);

        builder.Property(oie => oie.Quantity)
               .IsRequired();

        builder.Property(oie => oie.Quantity)
               .HasPrecision(10, 2)
               .IsRequired();
    }
}

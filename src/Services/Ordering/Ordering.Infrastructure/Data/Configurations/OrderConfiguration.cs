using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.ValueObjects.TypesIds;
using System.Linq.Expressions;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {

        #region Key
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
               .HasConversion(
                order => order.Value,
                dbId => OrderId.Of(dbId));
        #endregion

        #region relationship
        builder.HasOne<Customer>()
         .WithMany()
         .HasForeignKey(o => o.CustomerId)
         .IsRequired();

        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId);
        #endregion

        #region Complex Type Data
        builder.OwnsOne(o => o.OrderName, a =>
        {
            a.WithOwner();
            a.Property(n => n.Value).IsRequired();
        });


        builder.OwnsOne(o => o.Payment, a =>
        {
            a.WithOwner();
            a.Property(n => n.CardName).HasMaxLength(50).IsRequired();
            a.Property(n => n.CardNumber).HasMaxLength(50).IsRequired();
            a.Property(n => n.Expiration).HasMaxLength(50).IsRequired();
            a.Property(n => n.CVV).HasMaxLength(50).IsRequired();
            a.Property(n => n.PaymentMethod).HasMaxLength(50).IsRequired();

        });

        builder.Property(o => o.OrderStatus)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        MapComplexPropertyAddress(builder, o => o.ShippingAddress);
        MapComplexPropertyAddress(builder, o => o.BillingAddress);
        #endregion

        #region Properties
        builder.Property(o => o.TotalPrice)
               .HasPrecision(10, 2)
               .IsRequired();
        #endregion
    }

    private void MapComplexPropertyAddress(EntityTypeBuilder<Order> builder, Expression<Func<Order, Address?>> propertyExpression)
    {
        builder.OwnsOne(propertyExpression, addressBuilder =>
        {
            addressBuilder.WithOwner();

            addressBuilder.OwnsOne(c => c.CustomerName, customerNameBuilder =>
            {
                customerNameBuilder.Property(n => n.FirstName).HasMaxLength(100).IsRequired();
                customerNameBuilder.Property(n => n.LastName).HasMaxLength(100).IsRequired();
            });

            addressBuilder.Property(n => n.EmailAddress).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.AddressLine).HasMaxLength(180).IsRequired();
            addressBuilder.Property(n => n.Country).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.State).HasMaxLength(50).IsRequired();
            addressBuilder.Property(n => n.ZipCode).HasMaxLength(5).IsRequired();

        });
    }
}

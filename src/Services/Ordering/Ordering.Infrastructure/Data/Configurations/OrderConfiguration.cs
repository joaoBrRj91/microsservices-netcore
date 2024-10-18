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
        builder.ComplexProperty(
            o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
            });

        builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.CardName)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24)
                       .IsRequired();

                   paymentBuilder.Property(p => p.Expiration)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.CVV)
                       .HasMaxLength(3);

                   paymentBuilder.Property(p => p.PaymentMethod);
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

    private void MapComplexPropertyAddress(EntityTypeBuilder<Order> builder, Expression<Func<Order, Address>> propertyExpression)
    {
        builder.ComplexProperty(
             propertyExpression, addressBuilder =>
             {
                 #region Customer Name
                 addressBuilder.ComplexProperty(
                 c => c.CustomerName, customerNameBuilder =>
                 {
                     customerNameBuilder.Property(n => n.FirstName)
                     .HasMaxLength(100)
                     .IsRequired();

                     customerNameBuilder.Property(n => n.LastName)
                    .HasMaxLength(100)
                    .IsRequired();
                 });
                 #endregion

                 addressBuilder.Property(a => a.EmailAddress)
                     .HasMaxLength(50);

                 addressBuilder.Property(a => a.AddressLine)
                     .HasMaxLength(180)
                     .IsRequired();

                 addressBuilder.Property(a => a.Country)
                     .HasMaxLength(50);

                 addressBuilder.Property(a => a.State)
                     .HasMaxLength(50);

                 addressBuilder.Property(a => a.ZipCode)
                     .HasMaxLength(5)
                     .IsRequired();
             });
    }
}

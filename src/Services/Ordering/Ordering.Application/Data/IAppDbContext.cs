using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data;

public interface IAppDbContext
{
    DbSet<Customer> Customers { get; }
    DbSet<Product> Products { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<Order> Orders { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

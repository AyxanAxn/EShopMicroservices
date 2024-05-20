using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }
        DbSet<Customer> Customers { get; }
        DbSet<OrderItem> OrderItems { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
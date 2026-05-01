using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;

namespace Ordering.Application.Data
{
    public interface IApplicationDBContext
    {
        DbSet<Order> Orders {  get;}
        DbSet<OrderItem> OrderItems { get; }
        DbSet<Product> Products { get; }
        DbSet<Customer> Customers { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

using System;
using System.Collections.Generic;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data
{
    internal static class Initialdata
    {
        public static readonly List<Customer> Customers = new()
        {
            Customer.Create(CustomerId.Of(Guid.NewGuid()), "Alice Johnson", "alice.johnson@example.com"),
            Customer.Create(CustomerId.Of(Guid.NewGuid()), "Bob Smith", "bob.smith@example.com")
        };

        public static readonly List<Product> Products = new()
        {
            Product.Create(ProductId.Of(Guid.NewGuid()), "iPhone 13 Pro", 999.99m),
            Product.Create(ProductId.Of(Guid.NewGuid()), "Samsung Galaxy S21", 899.99m)
        };

        // Keep orders empty by default to avoid constructing complex domain objects here.
        public static readonly List<Order> OrdersWithItems = new();
    }
}
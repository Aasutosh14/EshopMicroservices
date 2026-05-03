using System;
using System.Collections.Generic;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Data
{
    internal static class Initialdata
    {
        public static readonly List<Customer> Customers = new()
        {
            Customer.Create(CustomerId.Of(Guid.Parse("3f2504e0-4f89-11d3-9a0c-0305e82c3301")), "Alice Johnson", "alice.johnson@example.com"),
            Customer.Create(CustomerId.Of(Guid.Parse("f47ac10b-58cc-4372-a567-0e02b2c3d479")), "Bob Smith", "bob.smith@example.com")
        };

        public static readonly List<Product> Products = new()
        {
            Product.Create(ProductId.Of(Guid.Parse("1a111111-1111-4111-8111-111111111111")), "iPhone 13 Pro", 999.99m),
            Product.Create(ProductId.Of(Guid.Parse("2b222222-2222-4222-8222-222222222222")), "Samsung Galaxy S21", 899.99m),
            Product.Create(ProductId.Of(Guid.Parse("3c333333-3333-4333-8333-333333333333")), "Google Pixel 6", 599.99m),
            Product.Create(ProductId.Of(Guid.Parse("4d444444-4444-4444-8444-444444444444")), "OnePlus 9", 729.00m)
        };

        public static readonly List<Order> OrdersWithItems = new();
    }
}
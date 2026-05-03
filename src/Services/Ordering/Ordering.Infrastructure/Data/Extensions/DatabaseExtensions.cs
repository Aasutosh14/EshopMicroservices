using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();
            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDBContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderandItemsAsync(context);
        }

        private static async Task SeedOrderandItemsAsync(ApplicationDBContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(Initialdata.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductAsync(ApplicationDBContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(Initialdata.Products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCustomerAsync(ApplicationDBContext context)
        {
           if(!await context.Customers.AnyAsync())
           {
               await context.Customers.AddRangeAsync(Initialdata.Customers);
               await context.SaveChangesAsync();
           }
        }
    }
}

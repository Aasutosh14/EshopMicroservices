using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if(await session.Query<Product>().AnyAsync(cancellation)) 
            {
                return;
            }
            session.Store<Product>(new List<Product>()
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "iPhone 13 Pro",
                    Category = new List<string> { "Smartphones", "Electronics" },
                    Description = "The iPhone 13 Pro features a sleek design, powerful A15 Bionic chip, and an advanced camera system for stunning photos and videos.",
                    ImageFile = "iphone13pro.jpg",
                    Price = 999.99m
                },
                new Product {
                    Id = Guid.NewGuid(),
                    Name = "Samsung Galaxy S21",
                    Category = new List<string> { "Smartphones", "Electronics" },
                    Description = "The Samsung Galaxy S21 offers a dynamic AMOLED display, powerful Exynos processor, and versatile camera system for capturing stunning photos and videos.",
                    ImageFile = "galaxys21.jpg",
                    Price = 899.99m
                }

            });
            await session.SaveChangesAsync();
        }
    }
}

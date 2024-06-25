using Marten.Schema;

namespace Catalog.API.Data
{
    //Developement Environment
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync(token: cancellation))
                return;

            //Marter UPSERT will cater for existing records
            session.Store(GetPreConfiguredProducts());
            await session.SaveChangesAsync(cancellation);   
        }

        public static IEnumerable<Product> GetPreConfiguredProducts() => new[]
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "IPhone 16",
                Description = "Last release of the smartphone Apple",
                ImageFile = "iphone-16.png",
                Price = 986.78M,
                Category = ["Smartphone", "Premium Product"]
            }
        };

    }
}

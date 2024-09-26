using Ordering.Domain.ValueObjects.TypesIds;

namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public string Name { get; private set; }
    public string Price { get; private set; }

}

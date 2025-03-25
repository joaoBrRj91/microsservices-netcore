namespace Shopping.Web.Models.Catalog;

internal class ProductModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public List<string> Category { get; set; } = new();
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}

//wrapper classes
internal record GetProductsResponse(IEnumerable<ProductModel> Products);
internal record GetProductByCategoryResponse(IEnumerable<ProductModel> Products);
internal record GetProductByIdResponse(ProductModel Product);
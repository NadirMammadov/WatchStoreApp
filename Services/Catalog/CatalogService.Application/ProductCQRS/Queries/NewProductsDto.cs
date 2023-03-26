namespace CatalogService.Application.ProductCQRS.Queries
{
    public class NewProductsDto
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public decimal Price { get; set; }
    }
}

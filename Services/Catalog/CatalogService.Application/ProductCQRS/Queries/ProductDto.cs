namespace CatalogService.Application.ProductCQRS.Queries
{
    public class ProductDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Brend { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string CorpusSize { get; set; } = null!;
        public string Mechanism { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}

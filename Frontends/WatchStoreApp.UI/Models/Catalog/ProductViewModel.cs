namespace WatchStoreApp.UI.Models.Catalog
{
    public class ProductViewModel
    {
        public List<ProductsModel> ProductsModel { get; set; } = null!;
        public ProductPageInfo ProductPageInformation { get; set; } = null!;
    }
    public class ProductsModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Brend { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string CorpusSize { get; set; } = null!;
        public string Mechanism { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public decimal Price { get; set; }
        public CategoryViewModel Category { get; set; } = null!;
    }
    public class ProductPageInfo
    {
        public long ProductCount { get; set; }
        public int TotalPage()
        {
            return (int)Math.Ceiling((decimal)ProductCount / 15);
        }
    }
}

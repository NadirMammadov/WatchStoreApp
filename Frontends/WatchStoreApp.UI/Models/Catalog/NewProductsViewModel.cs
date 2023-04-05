namespace WatchStoreApp.UI.Models.Catalog
{
    public class NewProductsViewModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Picture { get; set; } = null!;
        public string Brend { get; set; } = null!;
        public decimal Price { get; set; }
    }
}

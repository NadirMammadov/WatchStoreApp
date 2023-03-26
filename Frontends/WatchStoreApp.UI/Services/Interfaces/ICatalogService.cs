namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<ProductViewModel>> GetAllProductsAsync();
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<ProductViewModel>> GetAllProductByUserIdAsync(string userId);
        Task<bool> DeleteProductAsync(string productId);
        Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);
        Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput);
        Task<ProductViewModel> GetProductById(string productId);
        Task<List<NewProductsViewModel>> GetNewProducts();
    }
}

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<ProductViewModel> GetAllProductsAsync(int page);
        Task<List<CategoryViewModel>> GetAllCategoryAsync();
        Task<List<ProductViewModel>> GetAllProductByUserIdAsync(string userId);
        Task<bool> DeleteProductAsync(string productId);
        Task<bool> DeleteCategoryAsync(string categoryId);
        Task<bool> CreateProductAsync(ProductCreateInput productCreateInput);
        Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput);
        Task<bool> UpdateProductAsync(ProductUpdateModel productUpdateInput);
        Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput);
        Task<ProductsModel> GetProductById(string productId);
        Task<CategoryViewModel> GetCategoryById(string categoryId);
        Task<List<NewProductsViewModel>> GetNewProducts();
    }
}

using WastchStore.Shared.Dtos;

namespace WatchStoreApp.UI.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        public CatalogService(HttpClient client, IPhotoStockService photoStockService)
        {
            _client = client;
            _photoStockService = photoStockService;
        }

        #region Methods


        public async Task<bool> CreateProductAsync(ProductCreateInput productCreateInput)
        {
            var resultPhotoService = await _photoStockService.UploadPhoto(productCreateInput.PhotoFormFile);
            if (resultPhotoService != null)
            {
                productCreateInput.Picture = resultPhotoService.Url;
            }
            var response = await _client.PostAsJsonAsync<ProductCreateInput>("products", productCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> CreateCategoryAsync(CategoryCreateInput categoryCreateInput)
        {
            var response = await _client.PostAsJsonAsync<CategoryCreateInput>("categories", categoryCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteProductAsync(string productId)
        {
            var response = await _client.DeleteAsync($"products/{productId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DeleteCategoryAsync(string categoryId)
        {
            var response = await _client.DeleteAsync($"categories/{categoryId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _client.GetAsync("categories");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<List<ProductViewModel>> GetAllProductsAsync()
        {
            var response = await _client.GetAsync("products");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ProductViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<ProductViewModel> GetProductById(string productId)
        {
            var response = await _client.GetAsync($"products/{productId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<ProductViewModel>>();
            return responseSuccess.Data;
        }
        public async Task<CategoryViewModel> GetCategoryById(string categoryId)
        {
            var response = await _client.GetAsync($"categories/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<CategoryViewModel>>();
            return responseSuccess.Data;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateInput productUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<ProductUpdateInput>("products", productUpdateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<CategoryUpdateInput>("products", categoryUpdateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<ProductViewModel>> GetAllProductByUserIdAsync(string userId)
        {
            var response = await _client.GetAsync($"products/getallbyuserid/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<ProductViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<List<NewProductsViewModel>> GetNewProducts()
        {
            var response = await _client.GetAsync("products/getnewproducts");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<Response<List<NewProductsViewModel>>>();
            return responseSuccess.Data;
        }
        #endregion
    }
}

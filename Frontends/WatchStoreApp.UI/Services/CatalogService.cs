using WatchStore.Shared.Dtos;
using WatchStoreApp.UI.Helpers;

namespace WatchStoreApp.UI.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;
        public CatalogService(HttpClient client, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _client = client;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
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
            var responseSuccess = await response.Content.ReadFromJsonAsync<TResponse<List<CategoryViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<ProductViewModel> GetAllProductsAsync(int page)
        {
            var responseProducts = await _client.GetAsync($"products/get/page={page}");
            var responsePageInfo = await _client.GetAsync($"products/getproductcount");
            if (!responseProducts.IsSuccessStatusCode && !responsePageInfo.IsSuccessStatusCode)
            {
                return null;
            }
            var responseProductsSuccess = await responseProducts.Content.ReadFromJsonAsync<TResponse<List<ProductsModel>>>();
            var responsePageInfoSuccess = await responsePageInfo.Content.ReadFromJsonAsync<TResponse<ProductPageInfo>>();
            responseProductsSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            var responseData = new ProductViewModel
            {
                ProductsModel = responseProductsSuccess.Data,
                ProductPageInformation = responsePageInfoSuccess.Data
            };
            return responseData;
        }
        public async Task<ProductsModel> GetProductById(string productId)
        {
            var response = await _client.GetAsync($"products/{productId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<TResponse<ProductsModel>>();
            responseSuccess.Data.Picture = _photoHelper.GetPhotoStockUrl(responseSuccess.Data.Picture);
            return responseSuccess.Data;
        }
        public async Task<CategoryViewModel> GetCategoryById(string categoryId)
        {
            var response = await _client.GetAsync($"categories/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<TResponse<CategoryViewModel>>();
            return responseSuccess.Data;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateModel productUpdateModel)
        {
            if (productUpdateModel.PhotoFormFile != null)
            {
                var resultPhotoService = await _photoStockService.UploadPhoto(productUpdateModel.PhotoFormFile);
                if (resultPhotoService != null)
                {
                    await _photoStockService.DeletePhoto(productUpdateModel.Picture);
                    productUpdateModel.Picture = resultPhotoService.Url;
                }
            }
            var response = await _client.PutAsJsonAsync<ProductUpdateModel>("products", productUpdateModel);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateCategoryAsync(CategoryUpdateInput categoryUpdateInput)
        {
            var response = await _client.PutAsJsonAsync<CategoryUpdateInput>("categories", categoryUpdateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<List<ProductViewModel>> GetAllProductByUserIdAsync(string userId)
        {
            var response = await _client.GetAsync($"products/getallbyuserid/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<TResponse<List<ProductViewModel>>>();
            return responseSuccess.Data;
        }
        public async Task<List<NewProductsViewModel>> GetNewProducts()
        {
            var response = await _client.GetAsync("products/getnewproducts");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var responseSuccess = await response.Content.ReadFromJsonAsync<TResponse<List<NewProductsViewModel>>>();
            responseSuccess.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return responseSuccess.Data;
        }


        #endregion
    }
}

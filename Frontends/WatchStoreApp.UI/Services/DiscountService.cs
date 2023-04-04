using WatchStore.Shared.Dtos;
using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        private readonly IUserService _userService;

        public DiscountService(HttpClient httpClient, IUserService userService)
        {
            _httpClient = httpClient;
            _userService = userService;
        }
        public async Task<List<DiscountViewModel>> GetDiscountsByUserId()
        {
            var response = await _httpClient.GetAsync("discounts/GetByUserId/");
            if (!response.IsSuccessStatusCode)
                return null;
            var discount = await response.Content.ReadFromJsonAsync<TResponse<List<DiscountViewModel>>>();
            return discount.Data;
        }
        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            // [controller]/[action] /{ code}
            var response = await _httpClient.GetAsync($"discounts/getbycode/{discountCode}");
            if (!response.IsSuccessStatusCode)
                return null;
            var discount = await response.Content.ReadFromJsonAsync<TResponse<DiscountViewModel>>();
            return discount.Data;
        }

        public async Task<List<AdminDiscountViewModel>> GetDiscounts()
        {
            var response = await _httpClient.GetAsync("discounts");
            if (!response.IsSuccessStatusCode)
                return null;
            var discount = await response.Content.ReadFromJsonAsync<TResponse<List<AdminDiscountViewModel>>>();
            discount.Data.ForEach(x =>
            {
                x.UserName = _userService.GetUserName(x.UserId).Result.UserName.ToString();
            });
            return discount.Data;
        }
        public async Task<bool> DiscountCreate(DiscountCreateInput discountCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync<DiscountCreateInput>("discounts", discountCreateInput);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> DiscountDelete(int id)
        {
            var response = await _httpClient.DeleteAsync($"discounts/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

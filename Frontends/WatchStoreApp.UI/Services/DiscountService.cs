using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string discountCode)
        {
            // [controller]/[action] /{ code}
            var response = await _httpClient.GetAsync($"discounts/getbycode/{discountCode}");
            if (!response.IsSuccessStatusCode)
                return null;
            var discount = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();
            return discount.Data;
        }
    }
}

using WatchStore.Shared.Dtos;
using WatchStoreApp.UI.Models.Baskets;

namespace WatchStoreApp.UI.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;
        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            if (basketItemViewModel.Quantity == 0)
            {
                basketItemViewModel.Quantity = 1;
            }
            var basket = await Get();
            if (basket != null)
            {
                if (!basket.BasketItems.Any(x => x.ProductId == basketItemViewModel.ProductId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
                else
                {
                    basket.BasketItems.FirstOrDefault(x => x.ProductId == basketItemViewModel.ProductId).Quantity += 1;
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems.Add(basketItemViewModel);
            }
            await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();
            var basket = await Get();
            if (basket == null)
            {
                return false;
            }
            var hasDiscount = await _discountService.GetDiscount(discountCode);
            if (hasDiscount == null)
            {
                return false;
            }
            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);
            await SaveOrUpdate(basket);
            return true;
        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await Get();
            if (basket == null && basket.DiscountCode == null)
            {
                return false;
            }
            basket.CancelDiscount();
            await SaveOrUpdate(basket);
            return true;
        }

        public async Task<bool> Delete()
        {
            var result = await _httpClient.DeleteAsync("baskets");
            return result.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            var TResponse = await _httpClient.GetAsync("baskets");
            if (!TResponse.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await TResponse.Content.ReadFromJsonAsync<TResponse<BasketViewModel>>();
            return basketViewModel.Data;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var basket = await Get();
            if (basket == null)
            {
                return false;
            }
            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            if (deleteBasketItem == null)
                return false;
            var deleteResult = basket.BasketItems.Remove(deleteBasketItem);
            if (!deleteResult)
                return false;
            if (!basket.BasketItems.Any())
                basket.DiscountCode = null;
            return await SaveOrUpdate(basket);
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketViewModel)
        {
            var TResponse = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets", basketViewModel);
            return TResponse.IsSuccessStatusCode;
        }
    }
}

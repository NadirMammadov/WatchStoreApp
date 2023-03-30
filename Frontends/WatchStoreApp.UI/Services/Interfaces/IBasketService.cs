using WatchStoreApp.UI.Models.Baskets;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveOrUpdate(BasketViewModel basketViewModel);
        Task<BasketViewModel> Get();
        Task<bool> Delete();
        Task<bool> RemoveBasketItem(string productId);
        Task AddBasketItem(BasketItemViewModel basketItemViewModel);
        Task<bool> ApplyDiscount(string discountCode);
        Task<bool> CancelApplyDiscount();
    }
}

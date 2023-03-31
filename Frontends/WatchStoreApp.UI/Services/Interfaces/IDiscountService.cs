using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}

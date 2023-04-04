using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<List<DiscountViewModel>> GetDiscountsByUserId();
        Task<List<AdminDiscountViewModel>> GetDiscounts();
        Task<DiscountViewModel> GetDiscount(string discountCode);
        Task<bool> DiscountDelete(int id);
        Task<bool> DiscountCreate(DiscountCreateInput discountCreateInput);
    }
}

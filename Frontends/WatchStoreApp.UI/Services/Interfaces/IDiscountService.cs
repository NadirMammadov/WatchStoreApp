using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<List<DiscountViewModel>> GetDiscountsByUserId();
        Task<List<AdminDiscountViewModel>> GetDiscounts();
        Task<DiscountViewModel> GetDiscount(string discountCode);
        Task<DiscountViewModel> GetDiscountById(int id);
        Task<bool> DiscountUpdate(DiscountUpdateInput discountUpdateInput);
        Task<bool> DiscountDelete(int id);
        Task<bool> DiscountCreate(DiscountCreateInput discountCreateInput);
    }
}

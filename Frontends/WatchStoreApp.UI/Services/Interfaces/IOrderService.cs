using WatchStoreApp.UI.Models.Orders;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<OrderViewModel>> GetOrder();
    }
}

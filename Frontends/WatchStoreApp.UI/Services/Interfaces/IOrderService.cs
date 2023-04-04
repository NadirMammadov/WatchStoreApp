using WatchStoreApp.UI.Models.Orders;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<OrderViewModel> GetOrder(int orderId);
        Task<List<OrderViewModel>> GetOrders();
    }
}

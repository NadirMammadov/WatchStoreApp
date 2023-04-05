using WatchStoreApp.UI.Models.Orders;

namespace WatchStoreApp.UI.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);
        Task<List<AdminOrderViewModel>> GetOrders();
        Task<OrderViewModel> GetOrder(int orderId);
        Task<List<OrderViewModel>> GetOrdersByUserId();
    }
}

namespace WatchStoreApp.UI.ViewComponents
{
    public class OrdersViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;

        public OrdersViewComponent(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var orders = await _orderService.GetOrders();
            return View(orders);
        }

    }
}

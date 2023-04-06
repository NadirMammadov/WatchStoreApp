using Microsoft.AspNetCore.Authorization;
using WatchStoreApp.UI.Models.Orders;

namespace WatchStoreApp.UI.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrdersController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }
        public async Task<IActionResult> Detail(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            return View(order);
        }
        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;
            return View(new CheckoutInfoInput());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfoInput)
        {
            // var orderStatus = await _orderService.CreateOrder(checkoutInfoInput);
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfoInput);
            if (!orderSuspend.IsSuccessful)
            {
                var basket = await _basketService.Get();
                ViewBag.basket = basket;
                ViewBag.error = orderSuspend.Error;
                return View();
            }
            //return View(nameof(SuccessfulCheckout), new { orderId = orderSuspend.OrderId });
            return View(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1, 1000) });
        }
        public IActionResult SuccessfulCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }

        public async Task<IActionResult> CheckoutHistory()
        {
            return View(await _orderService.GetOrdersByUserId());
        }
    }
}

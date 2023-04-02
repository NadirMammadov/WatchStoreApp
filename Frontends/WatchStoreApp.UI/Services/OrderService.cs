using WatchStore.Shared.Dtos;
using WatchStore.Shared.Services;
using WatchStoreApp.UI.Models.Orders;
using WatchStoreApp.UI.Models.Payment;

namespace WatchStoreApp.UI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentService _paymentService;
        private readonly HttpClient _httpClient;
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _paymentService = paymentService;
            _httpClient = httpClient;
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<OrderCreatedViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = checkoutInfoInput.TotalPrice,
                CVV = checkoutInfoInput.CVV
            };
            var TResponsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!TResponsePayment)
            {
                return new OrderCreatedViewModel() { Error = "Ödəniş uğursuz oldu", IsSuccessful = false };
            }
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District, Street = checkoutInfoInput.Street, Line = checkoutInfoInput.Line, ZipCode = checkoutInfoInput.ZipCode },
            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { ProductId = x.ProductId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.ProductName };
                orderCreateInput.OrderItems.Add(orderItem);
            });
            var TResponse = await _httpClient.PostAsJsonAsync<OrderCreateInput>("orders", orderCreateInput);
            if (!TResponse.IsSuccessStatusCode)
            {
                return new OrderCreatedViewModel() { Error = "Sifariş uğursuz oldu", IsSuccessful = false };
            }
            var orderCreatedViewModel = await TResponse.Content.ReadFromJsonAsync<TResponse<OrderCreatedViewModel>>();
            orderCreatedViewModel.Data.IsSuccessful = true;
            await _basketService.Delete();
            return orderCreatedViewModel.Data;
        }

        public async Task<List<OrderViewModel>> GetOrder()
        {
            var TResponse = await _httpClient.GetFromJsonAsync<TResponse<List<OrderViewModel>>>("orders");
            return TResponse.Data;
        }

        public async Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput)
        {
            var basket = await _basketService.Get();
            var orderCreateInput = new OrderCreateInput()
            {
                BuyerId = _sharedIdentityService.GetUserId,
                Address = new AddressCreateInput { Province = checkoutInfoInput.Province, District = checkoutInfoInput.District, Street = checkoutInfoInput.Street, Line = checkoutInfoInput.Line, ZipCode = checkoutInfoInput.ZipCode },
            };
            basket.BasketItems.ForEach(x =>
            {
                var orderItem = new OrderItemCreateInput { ProductId = x.ProductId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.ProductName };
                orderCreateInput.OrderItems.Add(orderItem);
            });
            var paymentInfoInput = new PaymentInfoInput()
            {
                CardName = checkoutInfoInput.CardName,
                CardNumber = checkoutInfoInput.CardNumber,
                Expiration = checkoutInfoInput.Expiration,
                TotalPrice = checkoutInfoInput.TotalPrice,
                CVV = checkoutInfoInput.CVV,
                Order = orderCreateInput
            };
            var TResponsePayment = await _paymentService.ReceivePayment(paymentInfoInput);
            if (!TResponsePayment)
            {
                return new OrderSuspendViewModel() { Error = "Ödəniş uğursuz oldu", IsSuccessful = false };
            }
            await _basketService.Delete();
            return new OrderSuspendViewModel() { IsSuccessful = true };
        }
    }
}

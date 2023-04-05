using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Order.Commands;
using OrderService.Application.Order.Queries;
using WatchStore.Shared.ControllerBase;
using WatchStore.Shared.Services;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        private readonly ISharedIdentityService _sharedIdentityService;

        public OrdersController(ISharedIdentityService sharedIdentityService)
        {
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await Mediator.Send(new GetOrdersQuery());
            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/[action]")]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            var response = await Mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });
            return CreateActionResultInstance(response);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrdersById(int orderId)
        {
            var response = await Mediator.Send(new GetOrderByIdQuery(orderId));
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand command)
        {
            var response = await Mediator.Send(command);
            return CreateActionResultInstance(response);
        }
    }
}

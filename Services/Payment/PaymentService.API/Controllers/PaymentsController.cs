using MassTransit;
using Microsoft.AspNetCore.Mvc;
using PaymentService.API.Models;
using WatchStore.Shared.ControllerBase;
using WatchStore.Shared.Dtos;
using WatchStore.Shared.Messages;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public PaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
        {
            var sendEnpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));
            var createorderMessageCommand = new CreateOrderMessageCommand();
            createorderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
            createorderMessageCommand.District = paymentDto.Order.Address.District;
            createorderMessageCommand.Street = paymentDto.Order.Address.Street;
            createorderMessageCommand.Province = paymentDto.Order.Address.Province;
            createorderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;
            paymentDto.Order.OrderItems.ForEach(x =>
            {
                createorderMessageCommand.OrderItems.Add(new OrderItem
                {
                    PictureUrl = x.PictureUrl,
                    Price = x.Price,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName
                });
            });
            await sendEnpoint.Send<CreateOrderMessageCommand>(createorderMessageCommand);
            return CreateActionResultInstance(TResponse<NoContent>.Success(200));
        }
    }
}

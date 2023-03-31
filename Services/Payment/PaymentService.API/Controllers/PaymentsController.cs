using Microsoft.AspNetCore.Mvc;
using PaymentService.API.Models;
using WastchStore.Shared.Dtos;
using WatchStore.Shared.ControllerBase;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomControllerBase
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}

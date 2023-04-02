using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WatchStore.Shared.Dtos;

namespace WatchStore.Shared.ControllerBase
{
    public class CustomControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
        public IActionResult CreateActionResultInstance<T>(TResponse<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}

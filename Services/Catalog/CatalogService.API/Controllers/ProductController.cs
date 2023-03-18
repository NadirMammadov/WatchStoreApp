using CatalogService.Application.ProductCQRS.Commands;
using CatalogService.Application.ProductCQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using WatchStore.Shared.ControllerBase;

namespace PackageService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetProductsQuery());
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            var response = await Mediator.Send(command);
            return CreateActionResultInstance(response);
        }
    }
}

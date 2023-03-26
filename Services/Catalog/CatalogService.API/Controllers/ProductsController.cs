using CatalogService.Application.ProductCQRS.Commands;
using CatalogService.Application.ProductCQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using WatchStore.Shared.ControllerBase;

namespace PackageService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomControllerBase
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var response = await Mediator.Send(new GetProductQuery(id));
            return CreateActionResultInstance(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            var response = await Mediator.Send(command);
            return CreateActionResultInstance(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));
            return CreateActionResultInstance(response);
        }
        [HttpGet("getnewproducts")]

        public async Task<IActionResult> GetNewProducts()
        {
            var response = await Mediator.Send(new GetNewProductQuery());
            return CreateActionResultInstance(response);
        }
    }
}

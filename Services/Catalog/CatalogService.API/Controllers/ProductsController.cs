using CatalogService.Application.ProductCQRS.Commands;
using CatalogService.Application.ProductCQRS.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchStore.Shared.ControllerBase;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomControllerBase
    {
        [HttpGet]
        [Route("/api/[controller]/get/page={page}")]
        public async Task<IActionResult> Get(int page)
        {
            var response = await Mediator.Send(new GetProductsQuery(page));
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));

            return CreateActionResultInstance(response);
        }
        [HttpGet]
        [Route("/api/[controller]/getnewproducts")]
        public async Task<IActionResult> GetNewProducts()
        {
            var response = await Mediator.Send(new GetNewProductQuery());
            return CreateActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/getproductcount")]
        public async Task<IActionResult> GetProductCount()
        {
            var response = await Mediator.Send(new GetProductCountQuery());
            return CreateActionResultInstance(response);
        }
    }
}

using CatalogService.Application.CategoryCQRS.Commands;
using CatalogService.Application.CategoryCQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using WatchStore.Shared.ControllerBase;
namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await Mediator.Send(new GetCategoriesQuery());
            return CreateActionResultInstance(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await Mediator.Send(new GetCategoryQuery(id));
            return CreateActionResultInstance(response);
        }

        [HttpPut]

        public async Task<IActionResult> Update(UpdateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return CreateActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return CreateActionResultInstance(response);
        }
    }
}

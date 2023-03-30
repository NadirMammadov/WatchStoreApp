using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WatchStoreApp.UI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ICatalogService _catalogService;

        public AdminController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Products(int page = 1)
        {
            var products = await _catalogService.GetAllProductsAsync(page);
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductCreateInput productCreateInput)
        {
            var response = await _catalogService.CreateProductAsync(productCreateInput);
            return RedirectToAction("Products");
        }
        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryCreate(CategoryCreateInput categoryCreateInput)
        {
            _catalogService.CreateCategoryAsync(categoryCreateInput);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        [Route("admin/productUpdate/{productId}")]
        public async Task<IActionResult> ProductUpdate(string productId)
        {
            var response = await _catalogService.GetProductById(productId);

            var productUpdateModel = new ProductUpdateModel()
            {
                Id = response.Id,
                Name = response.Name,
                Description = response.Description,
                Brend = response.Brend,
                CorpusSize = response.CorpusSize,
                Model = response.Model,
                Mechanism = response.Mechanism,
                CategoryId = response.CategoryId,
                Price = response.Price,
                Picture = response.Picture
            };
            return View(productUpdateModel);
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductUpdateModel productUpdateModel)
        {
            await _catalogService.UpdateProductAsync(productUpdateModel);
            return RedirectToAction("Products");
        }
        [HttpGet]
        [Route("admin/categoryUpdate/{categoryId}")]
        public async Task<IActionResult> CategoryUpdate(string categoryId)
        {
            var response = await _catalogService.GetCategoryById(categoryId);

            var categoryUpdateInput = new CategoryUpdateInput()
            {
                Id = response.Id,
                Name = response.Name,
            };
            return View(categoryUpdateInput);
        }
        [HttpPost]
        public async Task<IActionResult> CategoryUpdate(CategoryUpdateInput categoryUpdateInput)
        {
            await _catalogService.UpdateCategoryAsync(categoryUpdateInput);
            return RedirectToAction("Categories");
        }
        [HttpGet]
        [Route("admin/productdelete/{productId}")]
        public async Task<IActionResult> ProductDelete(string productId)
        {
            await _catalogService.DeleteProductAsync(productId);
            return RedirectToAction("Products");
        }
        [HttpGet]
        [Route("admin/categorydelete/{categoryId}")]
        public async Task<IActionResult> CategoryDelete(string categoryId)
        {
            await _catalogService.DeleteCategoryAsync(categoryId);
            return RedirectToAction("Categories");
        }
    }
}

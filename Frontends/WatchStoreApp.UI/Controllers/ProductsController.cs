using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchStore.Shared.Services;

namespace WatchStoreApp.UI.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public ProductsController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllProductByUserIdAsync(_sharedIdentityService.GetUserId));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateInput productCreateInput)
        {
            var response = await _catalogService.CreateProductAsync(productCreateInput);
            return View();
        }
    }
}

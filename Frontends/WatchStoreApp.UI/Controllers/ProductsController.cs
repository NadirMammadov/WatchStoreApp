using WatchStore.Shared.Services;

namespace WatchStoreApp.UI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public ProductsController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _catalogService.GetAllProductsAsync(page);
            return View(response);
        }
        public async Task<IActionResult> Detail(string id)
        {
            var product = await _catalogService.GetProductById(id);
            return View(product);
        }
    }
}

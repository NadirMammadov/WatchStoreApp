using Microsoft.AspNetCore.Mvc;
using WatchStoreApp.UI.Models.Baskets;

namespace WatchStoreApp.UI.Controllers
{
    public class BasketController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public BasketController(ICatalogService catalogService, IBasketService basketService)
        {
            _catalogService = catalogService;
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _basketService.Get());
        }

        public async Task<IActionResult> AddBasketItem(string productId)
        {
            var product = await _catalogService.GetProductById(productId);
            var basketItem = new BasketItemViewModel { ProductId = product.Id, ProductName = product.Name, Price = product.Price, Quantity = 1 };
            await _basketService.AddBasketItem(basketItem);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            await _basketService.RemoveBasketItem(productId);
            return RedirectToAction(nameof(Index));
        }
    }
}

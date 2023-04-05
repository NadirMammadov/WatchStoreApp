using Microsoft.AspNetCore.Authorization;
using WatchStoreApp.UI.Models.Baskets;
using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Controllers
{
    [Authorize]
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
            var response = await _basketService.Get();
            return View(response);
        }

        //public async Task<IActionResult> AddBasketItem(string productId, int quantity = 1)
        //{
        //    var product = await _catalogService.GetProductById(productId);
        //    var basketItem = new BasketItemViewModel { ProductId = product.Id, ProductName = product.Name, PictureUrl = product.Picture, Price = product.Price, Quantity = quantity };
        //    await _basketService.AddBasketItem(basketItem);
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<IActionResult> AddBasketItem(AddBasketModel addBasketModel)
        {
            var product = await _catalogService.GetProductById(addBasketModel.ProductId);
            var basketItem = new BasketItemViewModel { ProductId = product.Id, ProductName = product.Name, PictureUrl = product.Picture, Price = product.Price, Quantity = addBasketModel.Quantity };
            await _basketService.AddBasketItem(basketItem);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveBasketItem(string productId)
        {
            await _basketService.RemoveBasketItem(productId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplyDiscount(DiscountApplyInput discountApplyInput)
        {
            if (!ModelState.IsValid)
            {
                TempData["discountError"] = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).First();
                return RedirectToAction(nameof(Index));
            }
            var discountStatus = await _basketService.ApplyDiscount(discountApplyInput.Code);
            TempData["discountStatus"] = discountStatus;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> CancelApplyDiscount()
        {
            await _basketService.CancelApplyDiscount();
            return RedirectToAction(nameof(Index));
        }
    }
}

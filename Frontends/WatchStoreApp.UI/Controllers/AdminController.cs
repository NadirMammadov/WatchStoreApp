using Microsoft.AspNetCore.Authorization;
using WatchStoreApp.UI.Models.Discount;

namespace WatchStoreApp.UI.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly IUserService _userService;
        private readonly IDiscountService _discountService;
        private readonly IOrderService _orderService;
        public AdminController(ICatalogService catalogService, IDiscountService discountService, IUserService userService, IOrderService orderService)
        {
            _catalogService = catalogService;
            _discountService = discountService;
            _userService = userService;
            _orderService = orderService;
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
            return RedirectToAction(nameof(Products));
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
            return RedirectToAction(nameof(Categories));
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
            return RedirectToAction(nameof(Products));
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
            return RedirectToAction(nameof(Categories));
        }
        [HttpGet]
        [Route("admin/productdelete/{productId}")]
        public async Task<IActionResult> ProductDelete(string productId)
        {
            await _catalogService.DeleteProductAsync(productId);
            return RedirectToAction(nameof(Products));
        }
        [HttpGet]
        [Route("admin/categorydelete/{categoryId}")]
        public async Task<IActionResult> CategoryDelete(string categoryId)
        {
            await _catalogService.DeleteCategoryAsync(categoryId);
            return RedirectToAction(nameof(Categories));
        }


        [HttpGet]
        public async Task<IActionResult> Discounts()
        {
            var response = await _discountService.GetDiscounts();
            return View(response);
        }

        [HttpGet]
        [Route("admin/discountdelete/{discountId}")]
        public async Task<IActionResult> DiscountDelete(int discountId)
        {
            var response = await _discountService.DiscountDelete(discountId);
            return RedirectToAction(nameof(Discounts));
        }
        [HttpGet]
        public IActionResult DiscountCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DiscountCreate(DiscountCreateInput discountCreateInput)
        {
            discountCreateInput.UserId = _userService.GetUserById(discountCreateInput.UserName).Result.Id.ToString();
            await _discountService.DiscountCreate(discountCreateInput);
            return RedirectToAction(nameof(Discounts));
        }

        [HttpGet]
        [Route("admin/discountUpdate/{discountId}")]
        public async Task<IActionResult> DiscountUpdate(int discountId)
        {
            var discount = await _discountService.GetDiscountById(discountId);
            var discountUpdateInput = new DiscountUpdateInput
            {
                Id = discount.Id,
                Code = discount.Code,
                Rate = discount.Rate,
                UserId = discount.UserId,
                UserName = _userService.GetUserName(discount.UserId).Result.UserName.ToString()
            };
            return View(discountUpdateInput);
        }
        [HttpPost]
        public async Task<IActionResult> DiscountUpdate(DiscountUpdateInput discountUpdateInput)
        {
            await _discountService.DiscountUpdate(discountUpdateInput);
            return RedirectToAction(nameof(Discounts));
        }


        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetOrders();
            return View(orders);
        }
        public async Task<IActionResult> OrderDetail(int orderId)
        {
            var order = await _orderService.GetOrder(orderId);
            return View(order);
        }
    }
}

namespace WatchStoreApp.UI.ViewComponents
{
    public class DiscountsViewComponent : ViewComponent
    {
        private readonly IDiscountService _discountService;

        public DiscountsViewComponent(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var orders = await _discountService.GetDiscountsByUserId();
            return View(orders);
        }

    }
}

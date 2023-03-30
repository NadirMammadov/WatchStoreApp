using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WatchStoreApp.UI.ViewComponents
{
    public class Categories : ViewComponent
    {
        private readonly ICatalogService _catalogService;

        public Categories(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            return View(new SelectList(categories, "Id", "Name"));
        }

    }
}

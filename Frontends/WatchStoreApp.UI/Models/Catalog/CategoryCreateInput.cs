using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Catalog
{
    public class CategoryCreateInput
    {
        [Display(Name = "Kateqoriyanın adı:")]
        public string Name { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Catalog
{
    public class ProductCreateInput
    {
        [Display(Name = "Məhsulun adı: ")]
        public string Name { get; set; } = null!;
        [Display(Name = "Məhsul haqqında: ")]
        public string? Description { get; set; }
        [Display(Name = "Məhsulun brendi: ")]
        public string Brend { get; set; } = null!;
        [Display(Name = "Məhsulun modeli: ")]
        public string Model { get; set; } = null!;
        [Display(Name = "Məhsulun korpus ölçüsü: ")]
        public string CorpusSize { get; set; } = null!;
        [Display(Name = "Məhsulun mexanizmi: ")]
        public string Mechanism { get; set; } = null!;
        public string Picture { get; set; } = null!;
        [Display(Name = "Məhsulun qiyməti: ")]
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = null!;
        [Display(Name = "Məhsulun Şəkli: ")]
        public IFormFile PhotoFormFile { get; set; } = null!;
    }
}

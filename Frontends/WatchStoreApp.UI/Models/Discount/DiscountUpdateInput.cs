using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Discount
{
    public class DiscountUpdateInput
    {
        public int Id { get; set; }
        [Display(Name = "Müştəri adı")]
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [Display(Name = "Endirim faizi")]
        public int Rate { get; set; }
        [Display(Name = "Endirim kodu")]
        public string Code { get; set; } = null!;
    }
}

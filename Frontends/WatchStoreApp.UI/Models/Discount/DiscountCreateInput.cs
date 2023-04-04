using System.ComponentModel.DataAnnotations;

namespace WatchStoreApp.UI.Models.Discount
{
    public class DiscountCreateInput
    {
        [Display(Name = "Müştəri adı")]
        public string UserName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        [Display(Name = "Endirim faizi")]
        public int Rate { get; set; }
        [Display(Name = "Endirim kodu")]
        public string Code { get; set; } = null!;
    }
}

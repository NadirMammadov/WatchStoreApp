namespace WatchStoreApp.UI.Models.Discount
{
    public class DiscountViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public int Rate { get; set; }
        public string Code { get; set; } = null!;
    }
}

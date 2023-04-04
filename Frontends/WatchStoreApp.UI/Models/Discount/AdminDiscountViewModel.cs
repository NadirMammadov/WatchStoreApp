namespace WatchStoreApp.UI.Models.Discount
{
    public class AdminDiscountViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string? UserName { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; } = null!;
    }
}

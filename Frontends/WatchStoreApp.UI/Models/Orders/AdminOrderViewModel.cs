namespace WatchStoreApp.UI.Models.Orders
{
    public class AdminOrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string BuyerId { get; set; } = null!;
        public string? UserName { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}

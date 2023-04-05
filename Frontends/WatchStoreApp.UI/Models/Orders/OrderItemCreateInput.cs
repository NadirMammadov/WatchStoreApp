namespace WatchStoreApp.UI.Models.Orders
{
    public class OrderItemCreateInput
    {
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

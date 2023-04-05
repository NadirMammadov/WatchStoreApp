namespace WatchStoreApp.UI.Models.Orders
{
    public class OrderItemViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }

        public decimal TotalPrice()
        {
            return (Quantity * Price);
        }
    }
}

namespace BasketService.API.Dtos
{
    public class BasketItemDto
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductId { get; set; } = null!;
        public string ProductName { get; set; } = null!;
    }
}

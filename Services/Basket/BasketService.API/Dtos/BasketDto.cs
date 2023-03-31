namespace BasketService.API.Dtos
{
    public class BasketDto
    {
        public BasketDto()
        {
            basketItems = new List<BasketItemDto>();
        }
        public string UserId { get; set; } = null!;
        public string? DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> basketItems { get; set; }
        public decimal TotalPrice
        {
            get => basketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}

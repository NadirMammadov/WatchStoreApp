namespace OrderService.Application.Dtos;
public class OrderDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public AddressDto Address { get; set; } = null!;
    public string BuyerId { get; set; } = null!;
    private readonly List<OrderItemDto> _orderItems;
    public IReadOnlyCollection<OrderItemDto> OrderItems => _orderItems;
    public OrderDto()
    {
        _orderItems = new List<OrderItemDto>();
    }
}

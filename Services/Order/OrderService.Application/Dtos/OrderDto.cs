using OrderService.Domain.OrderAggregate;

namespace OrderService.Application.Dtos;
public class OrderDto
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public Address Address { get; set; } = null!;
    public string BuyerId { get; set; } = null!;
    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
    public OrderDto()
    {
        _orderItems = new List<OrderItem>();
    }
}

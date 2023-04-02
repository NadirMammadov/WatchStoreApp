using MediatR;
using OrderService.Application.Dtos;
using OrderService.Domain.OrderAggregate;
using OrderService.Infrastructure;
using WatchStore.Shared.Dtos;

namespace OrderService.Application.Order.Commands
{
    public class CreateOrderCommand : IRequest<TResponse<CreatedOrderDto>>
    {
        public string BuyerId { get; set; } = null!;
        public List<OrderItemDto> OrderItems { get; set; } = null!;
        public AddressDto Address { get; set; } = null!;
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, TResponse<CreatedOrderDto>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Address newAddress = new Address(request.Address.Province, request.Address.District, request.Address.Street, request.Address.ZipCode, request.Address.Line);
            Domain.OrderAggregate.Order newOrder = new Domain.OrderAggregate.Order(request.BuyerId, newAddress);
            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });
            await _context.Orders.AddAsync(newOrder);
            await _context.SaveChangesAsync();
            return TResponse<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = newOrder.Id }, 201);
        }
    }
}

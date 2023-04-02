using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Dtos;
using OrderService.Application.Mappings;
using OrderService.Infrastructure;
using WatchStore.Shared.Dtos;

namespace OrderService.Application.Order.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<TResponse<List<OrderDto>>>
    {
        public string UserId { get; set; } = null!;
    }

    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, TResponse<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            if (!orders.Any())
            {
                return TResponse<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            return TResponse<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<OrderDto>>(orders), 200);
        }
    }
}

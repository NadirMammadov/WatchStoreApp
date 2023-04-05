using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Dtos;
using OrderService.Application.Mappings;
using OrderService.Infrastructure;
using WatchStore.Shared.Dtos;

namespace OrderService.Application.Order.Queries
{
    public class GetOrdersQuery : IRequest<TResponse<List<OrderDto>>> { }


    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, TResponse<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbContext;

        public GetOrdersQueryHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<TResponse<List<OrderDto>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems).ToListAsync();
            if (!orders.Any())
            {
                return TResponse<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }
            return TResponse<List<OrderDto>>.Success(ObjectMapper.Mapper.Map<List<OrderDto>>(orders), 200);
        }
    }

}

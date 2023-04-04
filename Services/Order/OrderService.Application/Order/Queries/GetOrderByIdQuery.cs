using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Dtos;
using OrderService.Application.Mappings;
using OrderService.Infrastructure;
using WatchStore.Shared.Dtos;

namespace OrderService.Application.Order.Queries
{
    public class GetOrderByIdQuery : IRequest<TResponse<OrderDto>>
    {
        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }

    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, TResponse<OrderDto>>
    {
        private readonly OrderDbContext _context;

        public GetOrderByIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<TResponse<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.Include(x => x.OrderItems).Where(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (order == null)
            {
                return TResponse<OrderDto>.Fail("Sifariş tapılmadı", 404);
            }
            return TResponse<OrderDto>.Success(ObjectMapper.Mapper.Map<OrderDto>(order), 200);
        }
    }
}

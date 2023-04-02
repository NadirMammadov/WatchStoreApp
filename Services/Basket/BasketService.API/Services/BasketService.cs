using BasketService.API.Dtos;
using System.Text.Json;
using WatchStore.Shared.Dtos;

namespace BasketService.API.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<TResponse<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? TResponse<bool>.Success(204) : TResponse<bool>.Fail("Basket not found", 404);
        }

        public async Task<TResponse<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return TResponse<BasketDto>.Fail("Basket not found", 404);
            }

            return TResponse<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<TResponse<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));

            return status ? TResponse<bool>.Success(204) : TResponse<bool>.Fail("Basket could not update or save", 500);
        }
    }
}

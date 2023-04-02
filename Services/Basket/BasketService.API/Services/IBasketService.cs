using BasketService.API.Dtos;
using WatchStore.Shared.Dtos;

namespace BasketService.API.Services
{
    public interface IBasketService
    {
        Task<TResponse<BasketDto>> GetBasket(string userId);
        Task<TResponse<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<TResponse<bool>> Delete(string userId);
    }
}

using DiscountService.API.Models;
using WatchStore.Shared.Dtos;

namespace DiscountService.API.Services
{
    public interface IDiscountService
    {
        Task<TResponse<List<Discount>>> GetAll();
        Task<TResponse<Discount>> GetById(int id);
        Task<TResponse<NoContent>> Save(Discount discount);
        Task<TResponse<NoContent>> Update(Discount discount);
        Task<TResponse<NoContent>> Delete(int id);
        Task<TResponse<Discount>> GetByCodeAndUserId(string code, string userId);

    }
}

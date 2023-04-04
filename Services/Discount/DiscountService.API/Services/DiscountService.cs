using Dapper;
using DiscountService.API.Models;
using Npgsql;
using System.Data;
using WatchStore.Shared.Dtos;

namespace DiscountService.API.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgroSql"));
        }

        public async Task<TResponse<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Idd", new { Idd = id });
            if (status > 0)
            {
                return TResponse<NoContent>.Success(204);
            }
            return TResponse<NoContent>.Fail("Discount not found ", StatusCodes.Status404NotFound);
        }

        public async Task<TResponse<List<Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Discount>("select * from discount");
            return TResponse<List<Discount>>.Success(discounts.ToList(), 200);
        }
        public async Task<TResponse<List<Discount>>> GetByUserId(string userId)
        {
            var discount = await _dbConnection.QueryAsync<Discount>("select * from discount where userid=@UserId", new { UserId = userId });
            var hasdicount = discount.ToList();
            if (hasdicount == null)
            {
                return TResponse<List<Discount>>.Fail("Discount not found", 404);
            }
            return TResponse<List<Discount>>.Success(hasdicount, 200);
        }
        public async Task<TResponse<Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Discount>("select * from discount where userid=@UserId and code = @Code", new { UserId = userId, Code = code });
            var hasDiscount = discount.FirstOrDefault();
            if (hasDiscount == null)
            {
                return TResponse<Discount>.Fail("Discount not found", 404);
            }
            return TResponse<Discount>.Success(hasDiscount, 200);
        }

        public async Task<TResponse<Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Discount>("select * from discount where id=@Id", new { id })).SingleOrDefault();
            if (discount == null)
            {
                return TResponse<Discount>.Fail("Discount not found", 404);
            }
            return TResponse<Discount>.Success(discount, 200);
        }

        public async Task<TResponse<NoContent>> Save(Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) Values (@userid,@rate,@code)", discount);
            if (saveStatus > 0)
            {
                return TResponse<NoContent>.Success(204);
            }
            return TResponse<NoContent>.Fail("an error accured while adding", 500);
        }

        public async Task<TResponse<NoContent>> Update(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id", discount);

            if (status > 0)
            {
                return TResponse<NoContent>.Success(204);
            }
            return TResponse<NoContent>.Fail("Discount not found", 404);
        }


    }
}

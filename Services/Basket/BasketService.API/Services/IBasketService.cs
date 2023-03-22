﻿using BasketService.API.Dtos;
using WastchStore.Shared.Dtos;

namespace BasketService.API.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
﻿namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GatBasket(string userName, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default);
    }
}

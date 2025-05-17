using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Models.Basket;
using StackExchange.Redis;

namespace Persistence.Respositories
{
    public class BasketRepository(IConnectionMultiplexer RedisConnection) : IBasketRepository
    {
        private readonly IDatabase _redis = RedisConnection.GetDatabase();
        
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var CreatedOrUpdatedBasket = await _redis.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30));
            
            if (CreatedOrUpdatedBasket) 
                return await GetBasketAsync(basket.Id);
            else
                return null;
            
        }

        public async Task<bool> DeleteBasketAysnc(string basketId) => await _redis.KeyDeleteAsync(basketId);

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var basket = await _redis.StringGetAsync(Key);
            if(string.IsNullOrEmpty(basket)) return null;

            return JsonSerializer.Deserialize<CustomerBasket>(basket!);

        }
    }
}

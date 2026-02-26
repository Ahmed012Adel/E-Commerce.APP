using E_Commerce.App.Domain.Contract.Infrastructre;
using E_Commerce.App.Domain.Entities.Basket;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.App.Infrastructre.Repositories
{
    public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();

        public async Task<CustomerBasket?> GetAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(basket);
            var update = await _database.StringSetAsync(basket.Id, value, timeToLive);
            if (update) return basket;

            return null;
        }
        public async Task<bool> DeleteAsync(string id) => await _database.KeyDeleteAsync(id);


    }
}
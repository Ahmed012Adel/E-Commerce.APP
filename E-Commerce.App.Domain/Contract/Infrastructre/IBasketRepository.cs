using E_Commerce.App.Domain.Entities.Basket;

namespace E_Commerce.App.Domain.Contract.Infrastructre
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetAsync(string id);
        Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan timeToLive);
        Task<bool> DeleteAsync(string id);
    }
}
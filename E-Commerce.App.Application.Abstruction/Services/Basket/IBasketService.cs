using E_Commerce.App.Application.Abstruction.Models.Basket;

namespace E_Commerce.App.Application.Abstruction.Services.Basket
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto);
        Task DeleteCustomerBasketAsync(string basketId);
    }
}
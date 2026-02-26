using E_Commerce.APIs.Controllers.Base;
using E_Commerce.App.Application.Abstruction.Models.Basket;
using E_Commerce.App.Application.Abstruction.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controller.Controllers.Basket
{
    public class BasketController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string id)
        {
            var basket = await serviceManager.BasketService.GetCustomerBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var basket = await serviceManager.BasketService.UpdateCustomerBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id) => await serviceManager.BasketService.DeleteCustomerBasketAsync(id);

    }
}
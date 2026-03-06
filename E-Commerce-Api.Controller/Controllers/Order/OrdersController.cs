using E_Commerce.APIs.Controllers.Base;
using E_Commerce.App.Application.Abstruction.Models.Orders;
using E_Commerce.App.Application.Abstruction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce_Api.Controller.Controllers.Order
{
    [Authorize]
    public class OrdersController(IServiceManager serviceManager) : BaseApiController
    {

        [HttpPost]
        public async Task<ActionResult<OrderToReturneDto>> CreateOrder (OrderToCreateDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.CreateOrderAsync(buyerEmail!, orderDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturneDto>>> GetOrderForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrdersForUserAsync(buyerEmail!);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<ActionResult<OrderToReturneDto>> GetOrderId (int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var result = await serviceManager.OrderService.GetOrderByIdAsync(id, buyerEmail!);
            return Ok(result);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<OrderToReturneDto>>> GetDeliveryMethods()
        {
            var result = await serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(result);
        }
    }
}

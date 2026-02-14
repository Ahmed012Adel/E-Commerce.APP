using E_Commerce.APIs.Controllers.Base;
using E_Commerce.App.Application.Abstruction.Models.Product;
using E_Commerce.App.Application.Abstruction.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controller.Controllers.Product
{
    public class ProductController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts()
        {
            var products = await serviceManager.ProductService.GetAllProductAsync();
            return Ok(products);
        }
         
    }
}

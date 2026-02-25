using E_Commerce.APIs.Controllers.Base;
using E_Commerce.App.Application.Abstruction.Models.Product;
using E_Commerce.App.Application.Abstruction.Services;
using E_Commerce_Api.Controller.Error;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controller.Controllers.Product
{
    public class ProductController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpGet] 
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
        {
            var products = await serviceManager.ProductService.GetAllProductAsync(specParams);
            return Ok(products);
        }
         
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProduct(id);

            if (product == null)
                return NotFound(new ApiResponse(404, $"The Product with Id: {id} is not found"));

            return Ok(product);
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<IEnumerable<VendorDto>>> GetBrands()
        {
            var brands = await serviceManager.ProductService.GetAllBrandAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await serviceManager.ProductService.GetAllCategoryAsync();
            return Ok(categories);
        }
    }
}

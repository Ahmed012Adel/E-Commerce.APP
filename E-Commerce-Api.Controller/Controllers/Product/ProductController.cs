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
         
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var product = await serviceManager.ProductService.GetProduct(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetBrands()
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

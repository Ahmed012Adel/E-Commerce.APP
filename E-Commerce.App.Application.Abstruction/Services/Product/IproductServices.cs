using E_Commerce.App.Application.Abstruction.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application.Abstruction.Services.Product
{
    public interface IproductServices
    {
        public Task<IEnumerable<ProductToReturnDto>> GetAllProductAsync(string? sort);
        public Task<ProductToReturnDto> GetProduct(int id);
        public Task<IEnumerable<BrandDto>> GetAllBrandAsync();
        public Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
    }
}

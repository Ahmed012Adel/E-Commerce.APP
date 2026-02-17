using AutoMapper;
using E_Commerce.App.Application.Abstruction.Models.Product;
using E_Commerce.App.Application.Abstruction.Services.Product;
using E_Commerce.App.Domain.Contract.Peresistence;
using E_Commerce.App.Domain.Entities.Product;
using E_Commerce.App.Domain.Specifications;

namespace E_Commerce.App.Application.Service.ProductServicies
{
    internal class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IproductServices
    {
        public async Task<IEnumerable<ProductToReturnDto>> GetAllProductAsync()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();
            var products = await unitOfWork.GetRepositieries<Product,int>().GetAllSpecAsync(spec);
            var ProductsMapped = mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products);
            return ProductsMapped;
        }

        public async Task<ProductToReturnDto> GetProduct(int id)
        {
            var spce = new ProductWithBrandAndCategorySpecifications(id);
            var product = await unitOfWork.GetRepositieries<Product,int>().GetWithSpecAsync(spce);
            var ProductMapped = mapper.Map<Product, ProductToReturnDto>(product);
            return ProductMapped;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandAsync()
        {
            var brands =await unitOfWork.GetRepositieries<ProductBrand, int>().GetAllAsync();
            var brandsMapped = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsMapped;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoryAsync()
        {
            var categories = await unitOfWork.GetRepositieries<ProductCategory, int>().GetAllAsync();
            var categoriesMapped = mapper.Map<IEnumerable<ProductCategory>, IEnumerable<CategoryDto>>(categories);
            return categoriesMapped;
        }

       
    }
}

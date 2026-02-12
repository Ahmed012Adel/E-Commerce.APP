using AutoMapper;
using E_Commerce.App.Application.Abstruction.Models.Product;
using E_Commerce.App.Domain.Entities.Product;

namespace E_Commerce.App.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name));
            
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}

using AutoMapper;
using E_Commerce.App.Application.Abstruction.Models.Product;
using E_Commerce.App.Domain.Entities.Product;

namespace E_Commerce.App.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand!.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductCategory, CategoryDto>();
        }
    }
}

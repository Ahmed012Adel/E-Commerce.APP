using AutoMapper;
using E_Commerce.App.Application.Abstruction.Models.Basket;
using E_Commerce.App.Application.Abstruction.Models.Product;
using E_Commerce.App.Domain.Entities.Basket;
using E_Commerce.App.Domain.Entities.Product;

namespace E_Commerce.App.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.vendor, opt => opt.MapFrom(src => src.vendor!.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Vendor, VendorDto>();
            CreateMap<ProductCategory, CategoryDto>();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }
    }
}

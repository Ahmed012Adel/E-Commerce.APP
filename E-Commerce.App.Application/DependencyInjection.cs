using AutoMapper;
using E_Commerce.App.Application.Abstruction.Services;
using E_Commerce.App.Application.Abstruction.Services.Basket;
using E_Commerce.App.Application.Mapping;
using E_Commerce.App.Application.Service;
using E_Commerce.App.Application.Service.BasketService;
using E_Commerce.App.Domain.Contract.Infrastructre;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.App.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicatinServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            
            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));
            services.AddScoped(typeof(Func<IBasketService>), (servicesProvider) =>
            {
                var mapper = servicesProvider.GetRequiredService<IMapper>();
                var config = servicesProvider.GetRequiredService<IConfiguration>();
                var basketRepository = servicesProvider.GetRequiredService<IBasketRepository>();

                return () => new BasketService(basketRepository, mapper, config);
            }
            );
            return services;
        }

    }
}

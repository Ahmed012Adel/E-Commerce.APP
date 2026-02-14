using E_Commerce.App.Domain.Contract;
using E_Commerce.App.Infrastructre.presistent._Data;
using E_Commerce.App.Infrastructre.presistent._Data.Interceptor;
using E_Commerce.App.Infrastructre.presistent.Repositieries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.App.Infrastructre.presistent
{
    public static class DependencyInjection 
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services,
                                                         IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options => 
            
            options
            .UseLazyLoadingProxies()
            .UseSqlServer(configuration.GetConnectionString("StoreContext")));
        
            services.AddScoped< IStroreContextIntializer , StoreContextIntializer>();

            services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOfWork));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

            return services;
       }

    }
}

using E_Commerce.App.Domain.Contract;
using E_Commerce.App.Infrastructre.presistent._Data;
using E_Commerce.App.Infrastructre.presistent._Data.Interceptor;
using E_Commerce.App.Infrastructre.presistent.Repositieries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Infrastructre.presistent
{
    public static class DependencyInjection 
    {
       public static IServiceCollection AddPersistenceService(this IServiceCollection services,
                                                        IConfiguration configuration)
       {
                services.AddDbContext<StoreDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("StoreContext")));

            services.AddScoped< IStroreContextIntializer , StoreContextIntializer>();

            services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOfWork));
            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CustomSaveChangesInterceptor));

            return services;
       }

    }
}

using LinkDev.Talabat.Infrastructrure.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.App.Infrastructure.presistent
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection service, IConfiguration configuration)
        {
            #region Store DbContext

            service.AddDbContext<StoreDbContxt>(optionBuilder =>
            {
                optionBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            service.AddScoped(typeof(IStoreContextIntializer), typeof(StoreDbContextntializer));
            service.AddScoped(typeof(ISaveChangesInterceptor), typeof(AuditInterceptor));
            #endregion

            service.AddDbContext<StoreIdentityDbContext>((seviceProvider , options) =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("IdentityContext"))
                    .AddInterceptors(seviceProvider.GetRequiredService<AuditInterceptor>());
            });

            service.AddScoped(typeof(AuditInterceptor));
            service.AddScoped<IStoreIdentityDbIntializer, StoreIdentityDbIntializer>();

            service.AddScoped(typeof(IUniteOfWork), typeof(UnitOfWork_Store_));

            return service;
        }
    }
}
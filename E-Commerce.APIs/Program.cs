using E_Commerce.APIs.Servicies;
using E_Commerce.App.Application;
using E_Commerce.App.Application.Abstruction;
using E_Commerce.App.Domain.Contract;
using E_Commerce.App.Infrastructre.presistent;
using E_Commerce.App.Infrastructre.presistent._Data;
using E_Commerce_Api.Controller;
using Microsoft.EntityFrameworkCore;
namespace E_Commerce.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var WebApplictionBuilder = WebApplication.CreateBuilder(args);
        
            
            #region Configuration Service

            // Add services to the container.

            WebApplictionBuilder.Services.AddControllers()
                .AddApplicationPart(typeof(ControllerAssemblyInformation).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            WebApplictionBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

            WebApplictionBuilder.Services.AddHttpContextAccessor();
            WebApplictionBuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));

            WebApplictionBuilder.Services.AddPersistenceService(WebApplictionBuilder.Configuration);
            WebApplictionBuilder.Services.AddApplicatinServices();



            #endregion

            var app = WebApplictionBuilder.Build();

            #region Update Database and Data Seeding

            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var stroreContext = service.GetRequiredService<IStroreContextIntializer>();

            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();
            //var LoggerFactoryLogger = service.GetRequiredService(typeof(ILoggerFactory));
            try
            {

                await stroreContext.UpdateDateBase();
                await stroreContext.SeedData(WebApplictionBuilder.Environment.ContentRootPath);
            }
            catch(Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>();
                logger.LogError("An error occurred while applying migrations or Data Seeding.");
                Console.WriteLine(ex);
            }

            #endregion

            #region Configuration Kestral Middelware

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}

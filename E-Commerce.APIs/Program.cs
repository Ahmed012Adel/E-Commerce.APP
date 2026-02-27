using E_Commerce.APIs.Middleware;
using E_Commerce.APIs.Servicies;
using E_Commerce.App.Application;
using E_Commerce.App.Application.Abstruction;
using E_Commerce.App.Application.Abstruction.Common;
using E_Commerce.App.Application.Abstruction.Models.Auth;
using E_Commerce.App.Application.Abstruction.Services.Auth;
using E_Commerce.App.Application.Service.Auth;
using E_Commerce.App.Domain.Contract.Peresistence.DbIntializer;
using E_Commerce.App.Domain.Entities.Identity;
using E_Commerce.App.Infrastructre;
using E_Commerce.App.Infrastructre.presistent;
using E_Commerce.App.Infrastructre.presistent._Data;
using E_Commerce.App.Infrastructre.presistent.Identity;
using E_Commerce_Api.Controller;
using E_Commerce_Api.Controller.Error;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace E_Commerce.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var WebApplicationBuilder = WebApplication.CreateBuilder(args);
        
            
            #region Configuration Service

            // Add services to the container.

            WebApplicationBuilder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(option => {
                    option.SuppressModelStateInvalidFilter = false;
                    option.InvalidModelStateResponseFactory = (actionContext) =>
                    {
                        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                                             .Select(P => new ApiValidationsErrorResponse.ValidationError() 
                                                             {
                                                                 Field = P.Key,
                                                                 Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
                                                             });

                        return new BadRequestObjectResult(new ApiValidationsErrorResponse() { Errors = errors});
                        
                    };
                })
                .AddApplicationPart(typeof(ControllerAssemblyInformation).Assembly);

            WebApplicationBuilder.Services.AddEndpointsApiExplorer().AddSwaggerGen();

            WebApplicationBuilder.Services.AddHttpContextAccessor();
            WebApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService) , typeof(LoggedInUserService));

            WebApplicationBuilder.Services.AddPersistenceService(WebApplicationBuilder.Configuration);
            WebApplicationBuilder.Services.AddApplicatinServices();
            WebApplicationBuilder.Services.AddInfrastructureServices(WebApplicationBuilder.Configuration);
            WebApplicationBuilder.Services.Configure<JWTSettings>(WebApplicationBuilder.Configuration.GetSection("JWTSettings"));

            WebApplicationBuilder.Services.AddIdentity<ApplicationsUser, IdentityRole>(Identityoptions => {

                //Identityoptions.SignIn.RequireConfirmedEmail = true;
                //Identityoptions.SignIn.RequireConfirmedPhoneNumber = true;
                //Identityoptions.SignIn.RequireConfirmedPhoneNumber = true;

                Identityoptions.Password.RequireNonAlphanumeric = true;
                Identityoptions.Password.RequiredUniqueChars = 2;
                Identityoptions.Password.RequiredLength = 6;
                Identityoptions.Password.RequireDigit = true;
                Identityoptions.Password.RequireLowercase = true;
                Identityoptions.Password.RequireUppercase = true;

                Identityoptions.Lockout.AllowedForNewUsers = true;
                Identityoptions.Lockout.MaxFailedAccessAttempts = 5;
                Identityoptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);

            }
            )
                .AddEntityFrameworkStores<StorIdentityDbContext>();

           




            #endregion

            var app = WebApplicationBuilder.Build();

            #region Update Database and Data Seeding

            var scope = app.Services.CreateScope();
            var service = scope.ServiceProvider;
            var stroreContext = service.GetRequiredService<IStroreContextIntializer>();
            var IdentityContext = service.GetRequiredService<IStoreIdentityContextIntializer>();

            var LoggerFactory = service.GetRequiredService<ILoggerFactory>();
            //var LoggerFactoryLogger = service.GetRequiredService(typeof(ILoggerFactory));
            try
            {

                await stroreContext.UpdateDateBase();
                await stroreContext.SeedData(WebApplicationBuilder.Environment.ContentRootPath);

                await IdentityContext.UpdateDateBase();
                await IdentityContext.SeedData();
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

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            #endregion
            app.Run();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
namespace E_Commerce.App.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var WebApplicationbuilder = WebApplication.CreateBuilder(args);

            #region Configuration Service
            // Add services to the container.

            WebApplicationbuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            WebApplicationbuilder.Services.AddEndpointsApiExplorer();
            WebApplicationbuilder.Services.AddSwaggerGen();


            var app = WebApplicationbuilder.Build();

            #endregion


            #region configuration Kestral Middelware

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


using System.Threading.Tasks;
using DomainLayer.Contracts;
using E_commerce.Web.CustomMiddleWares;
using E_commerce.Web.Extenstion;
using E_commerce.Web.Factory;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Service;


namespace E_commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();


            builder.Services.AddWebApplicationServices();
            builder.Services.AddJWTServices(builder.Configuration); 

            #endregion




            var app = builder.Build();


            #region Data Sedding

            await app.SeedDataBaseAsync();


            #endregion



            // Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWares();




            if (app.Environment.IsDevelopment())
            {
               app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();
            // for resource files like images and css
            app.UseStaticFiles();

            // order is important 
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            //


            app.MapControllers();

            app.Run();
        }
    }
}

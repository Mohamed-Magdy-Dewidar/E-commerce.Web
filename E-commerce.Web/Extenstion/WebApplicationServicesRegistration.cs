using DomainLayer.Contracts;
using E_commerce.Web.CustomMiddleWares;

namespace E_commerce.Web.Extenstion
{
    public static class WebApplicationServicesRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {

            using var Scope = app.Services.CreateScope();
            var DataSeedingObj = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedingObj.SeedDataAsync();

        }

        public static IApplicationBuilder UseCustomExceptionMiddleWares(this IApplicationBuilder app)
        {

            app.UseMiddleware<CustomExceptionHandlerMiddleWares>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}

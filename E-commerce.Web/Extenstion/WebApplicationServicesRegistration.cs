using DomainLayer.Contracts;
using E_commerce.Web.CustomMiddleWares;
using Swashbuckle.AspNetCore.SwaggerUI;
namespace E_commerce.Web.Extenstion
{
    public static class WebApplicationServicesRegistration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {

            using var Scope = app.Services.CreateScope();
            var DataSeedingObj = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await DataSeedingObj.SeedDataAsync();
            await DataSeedingObj.IdentitySeedDataAsync();

        }

        public static IApplicationBuilder UseCustomExceptionMiddleWares(this IApplicationBuilder app)
        {

            app.UseMiddleware<CustomExceptionHandlerMiddleWares>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(Options =>
            {
                Options.ConfigObject = new ConfigObject()
                {
                    DocExpansion = DocExpansion.None,
                    DisplayRequestDuration = true,
                    DefaultModelsExpandDepth = -1
                };
                Options.SwaggerEndpoint("/swagger/v1/swagger.json", "E-commerce API V1");
                Options.DocumentTitle = "E-commerce API Documentation";
                Options.EnableFilter();
                Options.EnablePersistAuthorization();

            });

            return app;
        }
    }
}

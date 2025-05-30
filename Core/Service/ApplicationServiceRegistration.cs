using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            //Services.AddScoped<IServiceManager, ServiceManager>();
            Services.AddScoped<IServiceManager, ServiceMangerWithFactoryDelegate>();
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<ICacheService, CacheService>();
            Services.AddScoped<IPaymentService, PaymentService>();
            //using Primary  constructor for ServiceManager


            Services.AddScoped<Func<IProductService>>(factory => () => factory.GetRequiredService<IProductService>());
           
            Services.AddScoped<Func<IBasketService>>(factory => () => factory.GetRequiredService<IBasketService>());
            
            Services.AddScoped<Func<IOrderService>>(factory => () => factory.GetRequiredService<IOrderService>());
            
            
            Services.AddScoped<Func<IAuthenticationService>>(factory => () => factory.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<Func<IPaymentService>>(factory => () => factory.GetRequiredService<IPaymentService>());



            // Using Factory Delegate for ServiceManager
            //Services.AddScoped<IServiceManager, ServiceMangerWithFactoryDelegate>(
            //    factory => new ServiceMangerWithFactoryDelegate(
            //        productFactory: () => factory.GetRequiredService<IProductService>(),
            //        basketFactory: () => factory.GetRequiredService<IBasketService>(),
            //        orderFactory: () => factory.GetRequiredService<IOrderService>(),
            //        authFactory: () => factory.GetRequiredService<IAuthenticationService>()
            //    ));


            return Services;
        }
    }
}

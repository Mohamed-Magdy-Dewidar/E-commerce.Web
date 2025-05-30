

using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Persistence.Identity;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructurePersistenceServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services,  IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<ICacheRepository, CacheRepository>();



            #region Redis Connection Service

            // 3. Redis Configuration (with error handling)
            var redisConnectionString = Configuration.GetConnectionString("RedisConnection");
            if (string.IsNullOrEmpty(redisConnectionString))
                throw new ArgumentNullException("RedisConnection string is missing in configuration");

            Services.AddSingleton<IConnectionMultiplexer>(_ =>
            {
                return ConnectionMultiplexer.Connect(redisConnectionString);

            });

            #endregion


            #region Identity Connection DB and Services

            Services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });


            //Manager<ApplicationUser> _serviceManager
            //RoleManager< IdentityRole > _roleManager 

            Services.AddIdentityCore<ApplicationUser>(options =>
            {

                //options.Lockout.MaxFailedAccessAttempts = 5;

            }).
                AddRoles<IdentityRole>().
                AddEntityFrameworkStores<StoreIdentityDbContext>();

            #endregion




            return Services;
        }
    }
}

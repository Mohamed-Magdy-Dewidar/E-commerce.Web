

using Microsoft.Extensions.Configuration;
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


            
            return Services;
        }
    }
}

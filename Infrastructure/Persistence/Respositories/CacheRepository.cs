using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Persistence.Respositories
{
    public class CacheRepository(IConnectionMultiplexer RedisConnection) : ICacheRepository
    {


        // repository for cache operations using Redis should only inculde add delete update
        // no logic like if key exists else do something else

        private readonly IDatabase _redis = RedisConnection.GetDatabase();


        public async Task<string?> GetAsync(string key)
        {
            var CacheValue = await _redis.StringGetAsync(key);
            return CacheValue.IsNullOrEmpty ? null : CacheValue.ToString();
            
        }

        public async Task SetAsync(string key, string CacheValue, TimeSpan TimeToLive)
        {
            await _redis.StringSetAsync(key, CacheValue, TimeToLive);
        }
   


    }
}

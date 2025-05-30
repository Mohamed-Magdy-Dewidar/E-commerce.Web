using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string CacheKey)
        {
            var CachedValue  = await _cacheRepository.GetAsync(CacheKey);
            if (!string.IsNullOrEmpty(CachedValue))
                return CachedValue;
            else
                return string.Empty;
        }

        public async Task SetAsync(string CacheKey, object CacheValue, TimeSpan TimeToLive)
        {
            var JsonCacheValue  = JsonSerializer.Serialize(CacheValue);
            if (string.IsNullOrEmpty(CacheKey))
                throw new ArgumentNullException(nameof(CacheKey), "Cache key cannot be null.");

            await _cacheRepository.SetAsync(CacheKey, JsonCacheValue ?? string.Empty, TimeToLive);

        }
    }
}

using FakeStoreNet.Application.Common;
using Microsoft.Extensions.Caching.Memory;

namespace FakeStoreNet.Infrastructure.Caching
{
    /// <summary>
    /// In-memory cache service implementation using IMemoryCache.
    /// </summary>
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Initializes a new instance of <see cref="MemoryCacheService"/>.
        /// </summary>
        /// <param name="memoryCache">The IMemoryCache instance.</param>
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <inheritdoc/>
        public Task<T?> GetAsync<T>(string key)
        {
            _memoryCache.TryGetValue(key, out T? value);
            return Task.FromResult(value);
        }

        /// <inheritdoc/>
        public Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            var options = new MemoryCacheEntryOptions();
            if (absoluteExpiration.HasValue)
            {
                options.SetAbsoluteExpiration(absoluteExpiration.Value);
            }
            if (slidingExpiration.HasValue)
            {
                options.SetSlidingExpiration(slidingExpiration.Value);
            }
            _memoryCache.Set(key, value, options);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task RemoveAsync(string key)
        {
            _memoryCache.Remove(key);
            return Task.CompletedTask;
        }
    }
}

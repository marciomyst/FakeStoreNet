using System;
using System.Threading.Tasks;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Infrastructure.Caching;
using Microsoft.Extensions.Caching.Memory;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Infrastructure.Tests
{
    /// <summary>
    /// Unit tests for MemoryCacheService to improve code coverage.
    /// </summary>
    public class MemoryCacheServiceTests
    {
        private readonly MemoryCacheService _cacheService;
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheServiceTests()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
            _cacheService = new MemoryCacheService(_memoryCache);
        }

        [Fact(DisplayName = "GetAsync returns null when key does not exist")]
        public async Task GetAsync_WhenKeyDoesNotExist_ReturnsNull()
        {
            var result = await _cacheService.GetAsync<string>("nonexistent");
            result.ShouldBeNull();
        }

        [Fact(DisplayName = "SetAsync and GetAsync store and retrieve a string value")]
        public async Task SetAsync_And_GetAsync_ReturnsValue()
        {
            var key = "testKey";
            var value = "testValue";
            await _cacheService.SetAsync(key, value);
            var result = await _cacheService.GetAsync<string>(key);
            result.ShouldBe(value);
        }

        [Fact(DisplayName = "RemoveAsync removes the cached entry")]
        public async Task RemoveAsync_RemovesValue()
        {
            var key = "toRemove";
            await _cacheService.SetAsync(key, 123);
            var before = await _cacheService.GetAsync<int>(key);
            before.ShouldBe(123);
            await _cacheService.RemoveAsync(key);
            var after = await _cacheService.GetAsync<int>(key);
            after.ShouldBe(0);
        }

        [Fact(DisplayName = "SetAsync with expirations sets entry options and value is retrievable before expiration")]
        public async Task SetAsync_WithExpirations_DoesNotThrow_And_ReturnsValueBeforeExpiration()
        {
            var key = "expiring";
            var value = new { Name = "John" };
            var absoluteExpiration = TimeSpan.FromMinutes(1);
            var slidingExpiration = TimeSpan.FromSeconds(30);

            await _cacheService.SetAsync(key, value, absoluteExpiration, slidingExpiration);
            var result = await _cacheService.GetAsync<object>(key);
            result.ShouldNotBeNull();
            result.ShouldBe(value);
        }
    }
}

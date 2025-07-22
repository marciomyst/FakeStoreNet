namespace FakeStoreNet.Application.Common
{
    /// <summary>
    /// Defines methods for interacting with a cache store.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Retrieves an entry from cache.
        /// </summary>
        /// <typeparam name="T">Type of cached value.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <returns>Cached value or default if not found.</returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// Stores an entry in cache.
        /// </summary>
        /// <typeparam name="T">Type of value to cache.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <param name="value">Value to cache.</param>
        /// <param name="absoluteExpiration">Absolute expiration timespan (optional).</param>
        /// <param name="slidingExpiration">Sliding expiration timespan (optional).</param>
        Task SetAsync<T>(string key, T value, TimeSpan? absoluteExpiration = null, TimeSpan? slidingExpiration = null);

        /// <summary>
        /// Removes an entry from cache.
        /// </summary>
        /// <param name="key">Cache key.</param>
        Task RemoveAsync(string key);
    }
}

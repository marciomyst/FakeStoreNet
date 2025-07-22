namespace FakeStoreNet.Application.Common
{
    /// <summary>
    /// Settings for cache expiration policies.
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// Absolute expiration in seconds for GetAllProductsQuery cache.
        /// </summary>
        public int GetAllProductsAbsoluteExpirationInSeconds { get; set; } = 60;
    }
}

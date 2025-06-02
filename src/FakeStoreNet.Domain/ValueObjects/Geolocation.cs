using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Encapsulates geographic coordinates.
    /// </summary>
    public sealed class Geolocation
    {
        /// <summary>
        /// Gets the latitude coordinate.
        /// </summary>
        public string Latitude { get; }

        /// <summary>
        /// Gets the longitude coordinate.
        /// </summary>
        public string Longitude { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Geolocation"/> value object.
        /// </summary>
        /// <param name="latitude">The latitude. Cannot be null or empty.</param>
        /// <param name="longitude">The longitude. Cannot be null or empty.</param>
        /// <exception cref="DomainValidationException">Thrown when <paramref name="latitude"/> or <paramref name="longitude"/> is null or whitespace.</exception>
        public Geolocation(string latitude, string longitude)
        {
            if (string.IsNullOrWhiteSpace(latitude))
                throw new DomainValidationException("Latitude is required");

            if (string.IsNullOrWhiteSpace(longitude))
                throw new DomainValidationException("Longitude is required");

            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance by value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not Geolocation other)
                return false;

            return string.Equals(Latitude, other.Latitude, StringComparison.Ordinal)
                && string.Equals(Longitude, other.Longitude, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on latitude and longitude.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Latitude, Longitude);
        }
    }
}

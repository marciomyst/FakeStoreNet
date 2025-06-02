using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a postal address.
    /// </summary>
    public sealed class Address
    {
        /// <summary>
        /// Gets the street name.
        /// </summary>
        public string Street { get; }

        /// <summary>
        /// Gets the street number.
        /// </summary>
        public string Number { get; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        public string City { get; }

        /// <summary>
        /// Gets the postal code.
        /// </summary>
        public string ZipCode { get; }

        /// <summary>
        /// Gets the geolocation.
        /// </summary>
        public Geolocation Geolocation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Address"/> value object.
        /// </summary>
        /// <param name="street">Street name. Cannot be null or empty.</param>
        /// <param name="number">Street number. Cannot be null or empty.</param>
        /// <param name="city">City name. Cannot be null or empty.</param>
        /// <param name="zipCode">Postal code. Cannot be null or empty.</param>
        /// <param name="geolocation">Geolocation coordinates. Cannot be null.</param>
        /// <exception cref="DomainValidationException">Thrown when any required parameter is invalid.</exception>
        public Address(string street, string number, string city, string zipCode, Geolocation geolocation)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new DomainValidationException("Street is required");

            if (string.IsNullOrWhiteSpace(number))
                throw new DomainValidationException("Number is required");

            if (string.IsNullOrWhiteSpace(city))
                throw new DomainValidationException("City is required");

            if (string.IsNullOrWhiteSpace(zipCode))
                throw new DomainValidationException("ZipCode is required");

            Geolocation = geolocation ?? throw new DomainValidationException("Geolocation is required");

            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance by value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not Address other)
                return false;

            return string.Equals(Street, other.Street, StringComparison.Ordinal)
                && string.Equals(Number, other.Number, StringComparison.Ordinal)
                && string.Equals(City, other.City, StringComparison.Ordinal)
                && string.Equals(ZipCode, other.ZipCode, StringComparison.Ordinal)
                && Equals(Geolocation, other.Geolocation);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on all properties.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Street, Number, City, ZipCode, Geolocation);
        }
    }
}

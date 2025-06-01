using System;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Encapsulates geographic coordinates in decimal form.
    /// </summary>
    public sealed class Geolocation : IEquatable<Geolocation>
    {
        public decimal Latitude { get; }
        public decimal Longitude { get; }

        public Geolocation(decimal latitude, decimal longitude)
        {
            if (latitude < -90m || latitude > 90m)
                throw new DomainValidationException("Latitude must be between -90 and 90");
            if (longitude < -180m || longitude > 180m)
                throw new DomainValidationException("Longitude must be between -180 and 180");

            Latitude = latitude;
            Longitude = longitude;
        }

        public bool Equals(Geolocation? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Latitude == other.Latitude && Longitude == other.Longitude;
        }

        public override bool Equals(object? obj) => Equals(obj as Geolocation);

        public override int GetHashCode() => HashCode.Combine(Latitude, Longitude);

        public static bool operator ==(Geolocation? left, Geolocation? right) => Equals(left, right);

        public static bool operator !=(Geolocation? left, Geolocation? right) => !Equals(left, right);
    }
}

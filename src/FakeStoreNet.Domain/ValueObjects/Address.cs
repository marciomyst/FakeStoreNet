using System;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a postal address.
    /// </summary>
    public sealed class Address : IEquatable<Address>
    {
        public string Street { get; }
        public string Number { get; }
        public string City { get; }
        public string ZipCode { get; }
        public Geolocation Geolocation { get; }

        public Address(
            string street,
            string number,
            string city,
            string zipCode,
            Geolocation geolocation)
        {
            if (string.IsNullOrWhiteSpace(street))
                throw new DomainValidationException("Street is required");
            if (string.IsNullOrWhiteSpace(number))
                throw new DomainValidationException("Number is required");
            if (string.IsNullOrWhiteSpace(city))
                throw new DomainValidationException("City is required");
            if (string.IsNullOrWhiteSpace(zipCode))
                throw new DomainValidationException("ZipCode is required");
            if (geolocation is null)
                throw new DomainValidationException("Geolocation is required");

            Street = street;
            Number = number;
            City = city;
            ZipCode = zipCode;
            Geolocation = geolocation;
        }

        public bool Equals(Address? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Street == other.Street
                && Number == other.Number
                && City == other.City
                && ZipCode == other.ZipCode
                && Geolocation.Equals(other.Geolocation);
        }

        public override bool Equals(object? obj) => Equals(obj as Address);

        public override int GetHashCode() =>
            HashCode.Combine(Street, Number, City, ZipCode, Geolocation);

        public static bool operator ==(Address? left, Address? right) => Equals(left, right);

        public static bool operator !=(Address? left, Address? right) => !Equals(left, right);
    }
}

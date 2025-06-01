using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.ValueObjects
{
    public class AddressTests
    {
        [Fact(DisplayName = "Ctor with valid data sets all properties")]
        public void Ctor_WithValidData_SetsProperties()
        {
            // Given
            string street = "Street";
            string number = "42";
            string city = "City";
            string zipCode = "12345";
var geo = new Geolocation(12.34m, 56.78m);

            // When
            var address = new Address(street, number, city, zipCode, geo);

            // Then
            address.Street.ShouldBe(street);
            address.Number.ShouldBe(number);
            address.City.ShouldBe(city);
            address.ZipCode.ShouldBe(zipCode);
            address.Geolocation.ShouldBe(geo);
        }

        [Theory(DisplayName = "Ctor with invalid street throws DomainValidationException")]
        [InlineData("", "Street is required")]
        [InlineData(null, "Street is required")]
        public void Ctor_InvalidStreet_ThrowsDomainValidationException(string? street, string expectedMessage)
        {
            // Given
            string number = "42";
            string city = "City";
            string zipCode = "12345";
var geo = new Geolocation(0m, 0m);

            // When
            Action act = () => new Address(street!, number, city, zipCode, geo);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe(expectedMessage);
        }

        [Theory(DisplayName = "Ctor with invalid number throws DomainValidationException")]
        [InlineData("", "Number is required")]
        [InlineData(null, "Number is required")]
        public void Ctor_InvalidNumber_ThrowsDomainValidationException(string? number, string expectedMessage)
        {
            // Given
            string street = "Street";
            string city = "City";
            string zipCode = "12345";
var geo = new Geolocation(0m, 0m);

            // When
            Action act = () => new Address(street, number!, city, zipCode, geo);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe(expectedMessage);
        }

        [Theory(DisplayName = "Ctor with invalid city throws DomainValidationException")]
        [InlineData("", "City is required")]
        [InlineData(null, "City is required")]
        public void Ctor_InvalidCity_ThrowsDomainValidationException(string? city, string expectedMessage)
        {
            // Given
            string street = "Street";
            string number = "42";
            string zipCode = "12345";
var geo = new Geolocation(0m, 0m);

            // When
            Action act = () => new Address(street, number, city!, zipCode, geo);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe(expectedMessage);
        }

        [Theory(DisplayName = "Ctor with invalid zipCode throws DomainValidationException")]
        [InlineData("", "ZipCode is required")]
        [InlineData(null, "ZipCode is required")]
        public void Ctor_InvalidZipCode_ThrowsDomainValidationException(string? zipCode, string expectedMessage)
        {
            // Given
            string street = "Street";
            string number = "42";
            string city = "City";
var geo = new Geolocation(0m, 0m);

            // When
            Action act = () => new Address(street, number, city, zipCode!, geo);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe(expectedMessage);
        }

        [Fact(DisplayName = "Ctor with null geolocation throws DomainValidationException")]
        public void Ctor_NullGeolocation_ThrowsDomainValidationException()
        {
            // Given
            string street = "Street";
            string number = "42";
            string city = "City";
            string zipCode = "12345";
            Geolocation geo = null!;

            // When
            Action act = () => new Address(street, number, city, zipCode, geo);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Geolocation is required");
        }

        [Fact(DisplayName = "Equals returns true for same values")]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Given
var geo = new Geolocation(12.34m, 56.78m);
            var a = new Address("Street", "42", "City", "12345", geo);
            var b = new Address("Street", "42", "City", "12345", geo);

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different values")]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Given
var geo1 = new Geolocation(12.34m, 56.78m);
var geo2 = new Geolocation(12.34m, 56.79m);
            var a = new Address("Street", "42", "City", "12345", geo1);
            var b = new Address("Street", "42", "City", "12345", geo2);

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

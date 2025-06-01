using System;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.ValueObjects
{
    public class GeolocationTests
    {
        [Fact(DisplayName = "Ctor with valid coordinates sets properties")]
        public void Ctor_WithValidCoordinates_SetsProperties()
        {
            decimal lat = 12.34m;
            decimal lon = 56.78m;

            var geo = new Geolocation(lat, lon);

            geo.Latitude.ShouldBe(lat);
            geo.Longitude.ShouldBe(lon);
        }

        [Theory(DisplayName = "Ctor with invalid latitude throws DomainValidationException")]
        [InlineData(-91, 0, "Latitude must be between -90 and 90")]
        [InlineData(91, 0, "Latitude must be between -90 and 90")]
        public void Ctor_InvalidLatitude_ThrowsDomainValidationException(decimal lat, decimal lon, string expectedMessage)
        {
            Action act = () => new Geolocation(lat, lon);

            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe(expectedMessage);
        }

        [Theory(DisplayName = "Ctor with invalid longitude throws DomainValidationException")]
        [InlineData(0, -181, "Longitude must be between -180 and 180")]
        [InlineData(0, 181, "Longitude must be between -180 and 180")]
        public void Ctor_InvalidLongitude_ThrowsDomainValidationException(decimal lat, decimal lon, string expectedMessage)
        {
            Action act = () => new Geolocation(lat, lon);

            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe(expectedMessage);
        }

        [Fact(DisplayName = "Equals returns true for same values")]
        public void Equals_SameValues_ReturnsTrue()
        {
            var a = new Geolocation(1m, 2m);
            var b = new Geolocation(1m, 2m);

            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different values")]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            var a = new Geolocation(1m, 2m);
            var b = new Geolocation(1m, 3m);

            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

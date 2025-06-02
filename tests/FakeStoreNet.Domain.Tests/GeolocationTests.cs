using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class GeolocationTests
    {
        [Fact(DisplayName = "Given valid latitude and longitude, when creating Geolocation, then properties are assigned")]
        public void GivenValidCoordinates_WhenCreatingGeolocation_ThenPropertiesAreAssigned()
        {
            // Given
            var lat = "12.3456";
            var lon = "-65.4321";

            // When
            var geo = new Geolocation(lat, lon);

            // Then
            geo.Latitude.ShouldBe(lat);
            geo.Longitude.ShouldBe(lon);
        }

        [Fact(DisplayName = "Given empty latitude, when creating Geolocation, then DomainValidationException is thrown")]
        public void GivenEmptyLatitude_WhenCreatingGeolocation_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Geolocation("", "0"))
                  .Message.ShouldBe("Latitude is required");
        }

        [Fact(DisplayName = "Given empty longitude, when creating Geolocation, then DomainValidationException is thrown")]
        public void GivenEmptyLongitude_WhenCreatingGeolocation_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Geolocation("0", ""))
                  .Message.ShouldBe("Longitude is required");
        }
    }
}

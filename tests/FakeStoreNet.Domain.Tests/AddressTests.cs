using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class AddressTests
    {
        private readonly Geolocation ValidGeo = new Geolocation("12.34", "56.78");

        [Fact(DisplayName = "Given valid address data, when creating Address, then properties are assigned")]
        public void GivenValidAddress_WhenCreatingAddress_ThenPropertiesAreAssigned()
        {
            // Given
            var street = "Main St";
            var number = "123";
            var city = "Springfield";
            var zip = "12345";

            // When
            var address = new Address(street, number, city, zip, ValidGeo);

            // Then
            address.Street.ShouldBe(street);
            address.Number.ShouldBe(number);
            address.City.ShouldBe(city);
            address.ZipCode.ShouldBe(zip);
            address.Geolocation.ShouldBe(ValidGeo);
        }

        [Theory(DisplayName = "Given missing required field, when creating Address, then DomainValidationException is thrown")]
        [InlineData("", "123", "City", "00000", "Street is required")]
        [InlineData("St", "", "City", "00000", "Number is required")]
        [InlineData("St", "123", "", "00000", "City is required")]
        [InlineData("St", "123", "City", "", "ZipCode is required")]
        public void GivenMissingField_WhenCreatingAddress_ThenDomainValidationException(string street, string number, string city, string zip, string expectedMessage)
        {
            Should.Throw<DomainValidationException>(() => new Address(street, number, city, zip, ValidGeo))
                .Message.ShouldBe(expectedMessage);
        }

        [Fact(DisplayName = "Given null geolocation, when creating Address, then DomainValidationException is thrown")]
        public void GivenNullGeolocation_WhenCreatingAddress_ThenDomainValidationException()
        {
            Should.Throw<DomainValidationException>(() => new Address("St", "1", "City", "00000", null))
                  .Message.ShouldBe("Geolocation is required");
        }
    }
}

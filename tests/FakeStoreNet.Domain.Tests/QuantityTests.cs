using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class QuantityTests
    {
        [Fact(DisplayName = "Given valid quantity, when creating Quantity, then Value is assigned")]
        public void GivenValidQuantity_WhenCreatingQuantity_ThenValueIsAssigned()
        {
            // Given
            int value = 5;

            // When
            var quantity = new Quantity(value);

            // Then
            quantity.Value.ShouldBe(value);
        }

        [Theory(DisplayName = "Given invalid quantity, when creating Quantity, then DomainValidationException is thrown")]
        [InlineData(0)]
        [InlineData(-1)]
        public void GivenInvalidQuantity_WhenCreatingQuantity_ThenDomainValidationExceptionIsThrown(int invalid)
        {
            Should.Throw<DomainValidationException>(() => new Quantity(invalid))
                  .Message.ShouldBe("Quantity must be >= 1");
        }
    }
}

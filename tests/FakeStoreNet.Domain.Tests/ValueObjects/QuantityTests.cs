using System;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.ValueObjects
{
    public class QuantityTests
    {
        [Fact(DisplayName = "Ctor with valid value sets property")]
        public void Ctor_WithValidValue_SetsProperty()
        {
            // Given
            int value = 1;

            // When
            var quantity = new Quantity(value);

            // Then
            quantity.Value.ShouldBe(value);
        }

        [Theory(DisplayName = "Ctor with invalid value throws DomainValidationException")]
        [InlineData(0)]
        [InlineData(-5)]
        public void Ctor_InvalidValue_ThrowsDomainValidationException(int invalidValue)
        {
            // Given

            // When
            Action act = () => new Quantity(invalidValue);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Quantity must be ≥ 1");
        }

        [Fact(DisplayName = "Equals returns true for same values")]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Given
            var a = new Quantity(3);
            var b = new Quantity(3);

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different values")]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Given
            var a = new Quantity(3);
            var b = new Quantity(4);

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

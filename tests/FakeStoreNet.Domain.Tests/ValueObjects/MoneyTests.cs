using System;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.ValueObjects
{
    public class MoneyTests
    {
        [Fact(DisplayName = "Ctor with valid amount and currency sets properties")]
        public void Ctor_WithValidAmountAndCurrency_SetsProperties()
        {
            // Given
            decimal amount = 100m;
            string currency = "USD";

            // When
            var money = new Money(amount, currency);

            // Then
            money.Amount.ShouldBe(amount);
            money.Currency.ShouldBe(currency);
        }

        [Fact(DisplayName = "Ctor with negative amount throws DomainValidationException")]
        public void Ctor_NegativeAmount_ThrowsDomainValidationException()
        {
            // Given
            decimal amount = -1m;
            string currency = "USD";

            // When
            Action act = () => new Money(amount, currency);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Amount must be ≥ 0");
        }

        [Fact(DisplayName = "Ctor with empty currency throws DomainValidationException")]
        public void Ctor_EmptyCurrency_ThrowsDomainValidationException()
        {
            // Given
            decimal amount = 10m;
            string currency = "";

            // When
            Action act = () => new Money(amount, currency);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Currency is required");
        }

        [Fact(DisplayName = "Equals returns true for same values")]
        public void Equals_SameValues_ReturnsTrue()
        {
            // Given
            var a = new Money(50m, "EUR");
            var b = new Money(50m, "EUR");

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different values")]
        public void Equals_DifferentValues_ReturnsFalse()
        {
            // Given
            var a = new Money(50m, "EUR");
            var b = new Money(51m, "EUR");

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

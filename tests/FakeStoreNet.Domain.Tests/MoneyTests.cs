using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class MoneyTests
    {
        [Fact(DisplayName = "Given valid amount and currency, when creating Money, then properties are assigned")]
        public void GivenValidAmountAndCurrency_WhenCreatingMoney_ThenPropertiesAreAssigned()
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

        [Fact(DisplayName = "Given negative amount, when creating Money, then DomainValidationException is thrown")]
        public void GivenNegativeAmount_WhenCreatingMoney_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Money(-50m, "USD"))
                  .Message.ShouldBe("Amount must be >= 0");
        }

        [Fact(DisplayName = "Given empty currency, when creating Money, then DomainValidationException is thrown")]
        public void GivenEmptyCurrency_WhenCreatingMoney_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Money(10m, ""))
                  .Message.ShouldBe("Currency is required");
        }
    }
}

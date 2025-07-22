using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with a currency.
    /// </summary>
    public sealed class Money
    {
        /// <summary>
        /// Gets the amount of money.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Gets the currency code (e.g., "USD").
        /// </summary>
        public string Currency { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Money"/> value object.
        /// </summary>
        /// <param name="amount">The monetary amount. Must be greater than or equal to zero.</param>
        /// <param name="currency">The currency code. Cannot be null or empty.</param>
        /// <exception cref="DomainValidationException">Thrown when <paramref name="amount"/> is negative or <paramref name="currency"/> is null or empty.</exception>
        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new DomainValidationException("Amount must be >= 0");

            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainValidationException("Currency is required");

            Amount = amount;
            Currency = currency;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance by value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Money other)
                return false;

            return Amount == other.Amount
                   && string.Equals(Currency, other.Currency, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on the amount and currency.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency?.ToUpperInvariant());
        }
    }
}

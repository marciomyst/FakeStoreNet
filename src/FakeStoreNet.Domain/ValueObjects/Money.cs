using System;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary amount with currency.
    /// </summary>
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new DomainValidationException("Amount must be ≥ 0");
            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainValidationException("Currency is required");

            Amount = amount;
            Currency = currency;
        }

        public bool Equals(Money? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object? obj) => Equals(obj as Money);

        public override int GetHashCode() => HashCode.Combine(Amount, Currency);

        public static bool operator ==(Money? left, Money? right) => Equals(left, right);

        public static bool operator !=(Money? left, Money? right) => !Equals(left, right);
    }
}

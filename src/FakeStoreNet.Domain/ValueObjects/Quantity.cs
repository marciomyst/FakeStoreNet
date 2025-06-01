using System;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a quantity of items.
    /// </summary>
    public sealed class Quantity : IEquatable<Quantity>
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value < 1)
                throw new DomainValidationException("Quantity must be ≥ 1");

            Value = value;
        }

        public bool Equals(Quantity? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value == other.Value;
        }

        public override bool Equals(object? obj) => Equals(obj as Quantity);

        public override int GetHashCode() => Value.GetHashCode();

        public static bool operator ==(Quantity? left, Quantity? right) => Equals(left, right);

        public static bool operator !=(Quantity? left, Quantity? right) => !Equals(left, right);
    }
}

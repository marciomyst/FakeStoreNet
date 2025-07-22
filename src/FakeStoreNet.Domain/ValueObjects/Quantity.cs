using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a quantity with minimum value of 1.
    /// </summary>
    public sealed class Quantity
    {
        /// <summary>
        /// Gets the quantity value.
        /// </summary>
        public int Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Quantity"/> value object.
        /// </summary>
        /// <param name="value">The quantity. Must be greater than or equal to 1.</param>
        /// <exception cref="DomainValidationException">Thrown when <paramref name="value"/> is less than 1.</exception>
        public Quantity(int value)
        {
            if (value < 1)
                throw new DomainValidationException("Quantity must be >= 1");

            Value = value;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance by value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Quantity other)
                return false;

            return Value == other.Value;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on the quantity.</returns>
        public override int GetHashCode() => Value.GetHashCode();
    }
}

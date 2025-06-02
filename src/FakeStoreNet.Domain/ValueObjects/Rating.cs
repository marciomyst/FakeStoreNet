using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Captures product rating metrics.
    /// </summary>
    public sealed class Rating
    {
        /// <summary>
        /// Gets the average rate (0.0–5.0).
        /// </summary>
        public double Rate { get; }

        /// <summary>
        /// Gets the number of ratings (≥ 0).
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> value object.
        /// </summary>
        /// <param name="rate">The average rate. Must be between 0.0 and 5.0.</param>
        /// <param name="count">The number of ratings. Must be zero or positive.</param>
        /// <exception cref="DomainValidationException">Thrown when rate or count are out of valid range.</exception>
        public Rating(double rate, int count)
        {
            if (rate < 0.0 || rate > 5.0)
                throw new DomainValidationException("Rate must be between 0.0 and 5.0");

            if (count < 0)
                throw new DomainValidationException("Count must be >= 0");

            Rate = rate;
            Count = count;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance by value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is not Rating other)
                return false;

            return Rate.Equals(other.Rate) && Count == other.Count;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on rate and count.</returns>
        public override int GetHashCode() => HashCode.Combine(Rate, Count);
    }
}

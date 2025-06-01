using System;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Captures product rating.
    /// </summary>
    public sealed class Rating : IEquatable<Rating>
    {
        public double Rate { get; }
        public int Count { get; }

        public Rating(double rate, int count)
        {
            if (rate < 0.0 || rate > 5.0)
                throw new DomainValidationException("Rate must be between 0.0 and 5.0");
            if (count < 0)
                throw new DomainValidationException("Count must be ≥ 0");

            Rate = rate;
            Count = count;
        }

        public bool Equals(Rating? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Math.Abs(Rate - other.Rate) < double.Epsilon && Count == other.Count;
        }

        public override bool Equals(object? obj) => Equals(obj as Rating);

        public override int GetHashCode() => HashCode.Combine(Rate, Count);

        public static bool operator ==(Rating? left, Rating? right) => Equals(left, right);

        public static bool operator !=(Rating? left, Rating? right) => !Equals(left, right);
    }
}

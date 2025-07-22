using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a user's full name.
    /// </summary>
    public sealed class Name
    {
        /// <summary>
        /// Gets the first name.
        /// </summary>
        public string FirstName { get; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Name"/> value object.
        /// </summary>
        /// <param name="firstName">The first name. Cannot be null or empty.</param>
        /// <param name="lastName">The last name. Cannot be null or empty.</param>
        /// <exception cref="DomainValidationException">Thrown when <paramref name="firstName"/> or <paramref name="lastName"/> is null or whitespace.</exception>
        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainValidationException("FirstName is required");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainValidationException("LastName is required");

            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Determines whether the specified object is equal to this instance by value.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Name other)
                return false;

            return string.Equals(FirstName, other.FirstName, StringComparison.Ordinal)
                && string.Equals(LastName, other.LastName, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A hash code based on first and last name.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName);
        }
    }
}

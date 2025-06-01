using System;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.ValueObjects
{
    /// <summary>
    /// Represents a user's full name.
    /// </summary>
    public sealed class Name : IEquatable<Name>
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Name(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new DomainValidationException("FirstName is required");
            if (string.IsNullOrWhiteSpace(lastName))
                throw new DomainValidationException("LastName is required");

            FirstName = firstName;
            LastName = lastName;
        }

        public bool Equals(Name? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FirstName == other.FirstName && LastName == other.LastName;
        }

        public override bool Equals(object? obj) => Equals(obj as Name);

        public override int GetHashCode() => HashCode.Combine(FirstName, LastName);

        public static bool operator ==(Name? left, Name? right) => Equals(left, right);

        public static bool operator !=(Name? left, Name? right) => !Equals(left, right);
    }
}

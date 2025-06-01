using System;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Primary entity representing an application user.
    /// </summary>
    public sealed class User : IEquatable<User>
    {
        public int Id { get; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public Name Name { get; }
        public Address Address { get; }

        public User(
            int id,
            string username,
            string email,
            string passwordHash,
            Name name,
            Address address)
        {
            if (id <= 0)
                throw new DomainValidationException("Id must be positive");
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainValidationException("Username is required");
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainValidationException("Email is required");
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainValidationException("PasswordHash is required");
            if (name is null)
                throw new DomainValidationException("Name is required");
            if (address is null)
                throw new DomainValidationException("Address is required");

            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            Address = address;
        }

        public bool Equals(User? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => Equals(obj as User);
        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(User? left, User? right) => Equals(left, right);
        public static bool operator !=(User? left, User? right) => !Equals(left, right);
    }
}

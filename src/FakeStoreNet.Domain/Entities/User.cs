using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents an application user.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Gets the username.
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Gets the email address.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the hashed password.
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Gets the user's name.
        /// </summary>
        public Name Name { get; private set; }

        /// <summary>
        /// Gets the user's address.
        /// </summary>
        public Address Address { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> entity.
        /// </summary>
        /// <param name="username">The username. Cannot be null or empty.</param>
        /// <param name="email">The email. Must be a valid email format.</param>
        /// <param name="passwordHash">The hashed password. Cannot be null or empty.</param>
        /// <param name="name">The name value object.</param>
        /// <param name="address">The address value object.</param>
        /// <exception cref="DomainValidationException">Thrown when any invariant is violated.</exception>
        public User(string username, string email, string passwordHash, Name name, Address address)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new DomainValidationException("Username is required");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainValidationException("Email is required");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainValidationException("PasswordHash is required");

            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Name = name ?? throw new DomainValidationException("Name is required");
            Address = address ?? throw new DomainValidationException("Address is required");
        }

        /// <summary>
        /// Updates the user's address.
        /// </summary>
        /// <param name="newAddress">The new address. Cannot be null.</param>
        public void UpdateAddress(Address newAddress)
        {
            Address = newAddress ?? throw new DomainValidationException("New address is required");
        }

        /// <summary>
        /// Changes the user's password hash.
        /// </summary>
        /// <param name="newPasswordHash">The new password hash. Cannot be null or empty.</param>
        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new DomainValidationException("New password hash is required");

            PasswordHash = newPasswordHash;
        }
    }
}

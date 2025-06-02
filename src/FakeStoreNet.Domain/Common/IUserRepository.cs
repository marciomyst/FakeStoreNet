using System.Collections.Generic;
using FakeStoreNet.Domain.Entities;

namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Repository interface for User aggregate.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a user by its identifier or throws an <see cref="EntityNotFoundException"/>.
        /// </summary>
        /// <param name="id">Identifier of the user.</param>
        /// <returns>User entity.</returns>
        User GetById(int id);

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>Collection of user entities.</returns>
        IEnumerable<User> GetAll();

        /// <summary>
        /// Persists a new user.
        /// </summary>
        /// <param name="user">User entity to add.</param>
        void Add(User user);
    }
}

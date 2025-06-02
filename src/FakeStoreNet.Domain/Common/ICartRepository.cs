using FakeStoreNet.Domain.Entities;

namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Repository interface for Cart aggregate.
    /// </summary>
    public interface ICartRepository
    {
        /// <summary>
        /// Retrieves a cart by its identifier or throws an <see cref="EntityNotFoundException"/>.
        /// </summary>
        /// <param name="id">Identifier of the cart.</param>
        /// <returns>Cart entity.</returns>
        Cart GetById(int id);

        /// <summary>
        /// Persists a new cart.
        /// </summary>
        /// <param name="cart">Cart entity to add.</param>
        void Add(Cart cart);

        /// <summary>
        /// Updates an existing cart.
        /// </summary>
        /// <param name="cart">Cart entity with updated values.</param>
        void Update(Cart cart);
    }
}

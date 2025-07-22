using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents an item in a shopping cart.
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        public int ProductId { get; }

        /// <summary>
        /// Gets the quantity of the product.
        /// </summary>
        public Quantity Quantity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItem"/> class.
        /// </summary>
        /// <param name="productId">The product identifier. Must be greater than zero.</param>
        /// <param name="quantity">The quantity of the product. Must be at least 1.</param>
        /// <exception cref="DomainValidationException">Thrown when <paramref name="productId"/> is invalid or <paramref name="quantity"/> is null.</exception>
        public CartItem(int productId, Quantity quantity)
        {
            if (productId <= 0)
                throw new DomainValidationException("ProductId must be greater than zero");

            Quantity = quantity ?? throw new DomainValidationException("Quantity is required");
            ProductId = productId;
        }

        /// <summary>
        /// Updates the quantity of the cart item.
        /// </summary>
        /// <param name="newQuantity">The new quantity value. Must be at least 1.</param>
        public void UpdateQuantity(int newQuantity)
        {
            Quantity = new Quantity(newQuantity);
        }
    }
}

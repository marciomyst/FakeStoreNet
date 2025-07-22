using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents an item within an order.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        public int ProductId { get; }

        /// <summary>
        /// Gets the quantity ordered.
        /// </summary>
        public Quantity Quantity { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItem"/> class.
        /// </summary>
        /// <param name="productId">The product identifier. Must be greater than zero.</param>
        /// <param name="quantity">The quantity. Must be at least 1.</param>
        /// <exception cref="DomainValidationException">Thrown when productId is invalid or quantity is null.</exception>
        public OrderItem(int productId, Quantity quantity)
        {
            if (productId <= 0)
                throw new DomainValidationException("ProductId must be greater than zero");

            Quantity = quantity ?? throw new DomainValidationException("Quantity is required");
            ProductId = productId;
        }

        /// <summary>
        /// Updates the quantity of this order item.
        /// </summary>
        /// <param name="newQuantity">The new quantity. Must be at least 1.</param>
        public void UpdateQuantity(int newQuantity)
        {
            Quantity = new Quantity(newQuantity);
        }
    }
}

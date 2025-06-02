using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents a shopping cart aggregate.
    /// </summary>
    public class Cart : Entity
    {
        /// <summary>
        /// Gets the user identifier owning this cart.
        /// </summary>
        public int UserId { get; private set; }

        /// <summary>
        /// Gets the creation date of the cart.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the list of items in the cart.
        /// </summary>
        public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

        private readonly List<CartItem> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cart"/> aggregate.
        /// </summary>
        /// <param name="userId">The user identifier. Must be greater than zero.</param>
        public Cart(int userId)
        {
            if (userId <= 0)
                throw new DomainValidationException("UserId must be greater than zero");

            UserId = userId;
            Date = DateTime.UtcNow;
            _items = new List<CartItem>();
        }

        /// <summary>
        /// Adds an item to the cart or updates its quantity if it already exists.
        /// </summary>
        /// <param name="productId">The product identifier. Must be greater than zero.</param>
        /// <param name="quantity">The quantity to add. Must be at least 1.</param>
        public void AddItem(int productId, int quantity)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                _items.Add(new CartItem(productId, new ValueObjects.Quantity(quantity)));
            }
            else
            {
                item.UpdateQuantity(item.Quantity.Value + quantity);
            }
        }

        /// <summary>
        /// Removes an item from the cart.
        /// </summary>
        /// <param name="productId">The product identifier to remove.</param>
        public void RemoveItem(int productId)
        {
            var item = _items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                _items.Remove(item);
        }
    }
}

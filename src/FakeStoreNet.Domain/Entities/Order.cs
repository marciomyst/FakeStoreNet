using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents a customer order aggregate.
    /// </summary>
    public class Order : Entity
    {
        private readonly List<OrderItem> _items;

        /// <summary>
        /// Gets the identifier of the user who placed the order.
        /// </summary>
        public int UserId { get; private set; }

        /// <summary>
        /// Gets the date and time when the order was created.
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Gets the collection of items in this order.
        /// </summary>
        public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

        /// <summary>
        /// Gets a value indicating whether the order has been submitted.
        /// </summary>
        public bool IsSubmitted { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> aggregate.
        /// </summary>
        /// <param name="userId">The identifier of the user placing the order. Must be greater than zero.</param>
        /// <param name="items">The list of order items. Must contain at least one item.</param>
        /// <exception cref="DomainValidationException">
        /// Thrown when <paramref name="userId"/> is invalid or <paramref name="items"/> is null or empty.
        /// </exception>
        public Order(int userId, IEnumerable<OrderItem> items)
        {
            if (userId <= 0)
                throw new DomainValidationException("UserId must be greater than zero");

            if (items == null)
                throw new DomainValidationException("Items are required");

            _items = new List<OrderItem>(items);
            if (_items.Count == 0)
                throw new DomainValidationException("At least one order item is required");

            UserId = userId;
            Date = DateTime.UtcNow;
            IsSubmitted = false;
        }

        /// <summary>
        /// Marks the order as submitted.
        /// </summary>
        /// <exception cref="DomainValidationException">Thrown when the order is already submitted.</exception>
        public void SubmitOrder()
        {
            if (IsSubmitted)
                throw new DomainValidationException("Order is already submitted");

            IsSubmitted = true;
        }
    }
}

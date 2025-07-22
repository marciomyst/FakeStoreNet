namespace FakeStoreNet.Domain.Common.Events
{
    /// <summary>
    /// Fired when an existing product is updated.
    /// </summary>
    public sealed record ProductUpdatedEvent : IDomainEvent
    {
        /// <summary>
        /// Unique identifier for this event.
        /// </summary>
        public Guid EventId { get; init; } = Guid.NewGuid();

        /// <summary>
        /// Date and time when the event occurred (UTC).
        /// </summary>
        public DateTime OccurredOn { get; init; } = DateTime.UtcNow;

        /// <summary>
        /// Identifier of the aggregate (product) that raised this event.
        /// </summary>
        public int AggregateId { get; init; }

        /// <summary>
        /// Gets the product identifier.
        /// </summary>
        public int ProductId { get; init; }

        /// <summary>
        /// Gets the updated title of the product.
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Gets the updated price of the product.
        /// </summary>
        public decimal Price { get; init; }

        /// <summary>
        /// Gets the updated category of the product.
        /// </summary>
        public string Category { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductUpdatedEvent"/> record.
        /// </summary>
        /// <param name="aggregateId">Aggregate identifier.</param>
        /// <param name="productId">Product identifier.</param>
        /// <param name="title">Product title.</param>
        /// <param name="price">Product price.</param>
        /// <param name="category">Product category.</param>
        public ProductUpdatedEvent(int aggregateId, int productId, string title, decimal price, string category)
        {
            AggregateId = aggregateId;
            ProductId = productId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Price = price;
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }
    }
}

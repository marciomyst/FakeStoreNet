namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Represents a domain event with metadata for dispatching.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets the unique identifier for this event.
        /// </summary>
        Guid EventId { get; }

        /// <summary>
        /// Gets the date and time when the event occurred (UTC).
        /// </summary>
        DateTime OccurredOn { get; }

        /// <summary>
        /// Gets the identifier of the aggregate that raised this event.
        /// </summary>
        int AggregateId { get; }
    }
}

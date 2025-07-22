namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Defines an event bus for publishing and subscribing to domain events.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Publishes a domain event asynchronously to all registered subscribers.
        /// </summary>
        /// <typeparam name="TEvent">Type of the domain event.</typeparam>
        /// <param name="evt">Event instance to publish.</param>
        Task PublishAsync<TEvent>(TEvent evt) where TEvent : IDomainEvent;

        /// <summary>
        /// Subscribes a handler to be invoked when events of the specified type are published.
        /// </summary>
        /// <typeparam name="TEvent">Type of the domain event.</typeparam>
        /// <param name="handler">Asynchronous handler to invoke on event publication.</param>
        void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IDomainEvent;
    }
}

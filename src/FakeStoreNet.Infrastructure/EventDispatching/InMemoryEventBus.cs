using FakeStoreNet.Domain.Common;
using System.Collections.Concurrent;

namespace FakeStoreNet.Infrastructure.EventDispatching
{
    /// <summary>
    /// In-memory implementation of <see cref="IEventBus"/> for dispatching domain events.
    /// </summary>
    public class InMemoryEventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, List<Func<IDomainEvent, Task>>> _handlers
            = new();

        /// <inheritdoc/>
        public Task PublishAsync<TEvent>(TEvent evt) where TEvent : IDomainEvent
        {
            var eventType = typeof(TEvent);
            if (_handlers.TryGetValue(eventType, out var handlers))
            {
                var tasks = new List<Task>();
                foreach (var handler in handlers)
                {
                    tasks.Add(handler(evt));
                }
                return Task.WhenAll(tasks);
            }
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public void Subscribe<TEvent>(Func<TEvent, Task> handler) where TEvent : IDomainEvent
        {
            var eventType = typeof(TEvent);
            var wrapper = new Func<IDomainEvent, Task>(e => handler((TEvent)e));
            _handlers.AddOrUpdate(
                eventType,
                new List<Func<IDomainEvent, Task>> { wrapper },
                (_, existing) =>
                {
                    existing.Add(wrapper);
                    return existing;
                });
        }
    }
}

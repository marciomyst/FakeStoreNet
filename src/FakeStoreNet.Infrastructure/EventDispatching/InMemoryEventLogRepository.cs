using System.Collections.Concurrent;
using System.Threading.Tasks;
using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Infrastructure.EventDispatching
{
    /// <summary>
    /// In-memory implementation of <see cref="IEventLogRepository"/> for storing event logs.
    /// </summary>
    public class InMemoryEventLogRepository : IEventLogRepository
    {
        private readonly ConcurrentBag<EventLog> _logs = new();

        /// <inheritdoc/>
        public Task SaveAsync(EventLog eventLog)
        {
            _logs.Add(eventLog);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets all saved event logs.
        /// </summary>
        public IReadOnlyCollection<EventLog> Logs => _logs.ToArray();
    }
}

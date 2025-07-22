namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Repository interface for persisting event logs.
    /// </summary>
    public interface IEventLogRepository
    {
        /// <summary>
        /// Saves an event log entry asynchronously.
        /// </summary>
        /// <param name="eventLog">The event log to save.</param>
        Task SaveAsync(EventLog eventLog);
    }
}

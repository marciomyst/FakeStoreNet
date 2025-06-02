using System;

namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Represents a persisted log entry for a domain event.
    /// </summary>
    public sealed class EventLog
    {
        /// <summary>
        /// Unique identifier for this event log entry.
        /// </summary>
        public Guid Id { get; init; } = Guid.NewGuid();

        /// <summary>
        /// The CLR type name of the event.
        /// </summary>
        public string EventType { get; init; }

        /// <summary>
        /// The serialized JSON payload of the event.
        /// </summary>
        public string Payload { get; init; }

        /// <summary>
        /// The date and time when the event occurred (UTC).
        /// </summary>
        public DateTime OccurredOn { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventLog"/> class.
        /// </summary>
        /// <param name="eventType">Type name of the event.</param>
        /// <param name="payload">Serialized JSON payload.</param>
        /// <param name="occurredOn">UTC date and time when event occurred.</param>
        public EventLog(string eventType, string payload, DateTime occurredOn)
        {
            EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
            Payload = payload ?? throw new ArgumentNullException(nameof(payload));
            OccurredOn = occurredOn;
        }
    }
}

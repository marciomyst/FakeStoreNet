using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Common.Events;
using FakeStoreNet.Infrastructure.EventDispatching;
using System.Text.Json;

namespace FakeStoreNet.Infrastructure.Tests
{
    /// <summary>
    /// Tests for in-memory event bus and log repository integration.
    /// </summary>
    public class EventDispatchingTests
    {
        [Fact(DisplayName = "Given subscription and repository, when publishing ProductCreatedEvent, then event is logged")]
        public async Task GivenSubscriptionAndRepository_WhenPublishingProductCreatedEvent_ThenEventIsLogged()
        {
            // Given
            var eventBus = new InMemoryEventBus();
            var eventLogRepo = new InMemoryEventLogRepository();
            eventBus.Subscribe<ProductCreatedEvent>(async evt =>
            {
                var payload = JsonSerializer.Serialize(evt);
                var log = new EventLog(evt.GetType().FullName!, payload, evt.OccurredOn);
                await eventLogRepo.SaveAsync(log);
            });

            var aggregateId = 1;
            var productId = 1;
            var title = "Test";
            var price = 5.0m;
            var category = "Cat";
            var evt = new ProductCreatedEvent(aggregateId, productId, title, price, category);

            // When
            await eventBus.PublishAsync(evt);

            // Then
            eventLogRepo.Logs.ShouldNotBeEmpty();
            var saved = eventLogRepo.Logs.First();
            saved.EventType.ShouldBe(evt.GetType().FullName);
            saved.Payload.ShouldContain(title);
            saved.Payload.ShouldContain(category);
            saved.OccurredOn.ShouldBe(evt.OccurredOn);
        }
    }
}


using FakeStoreNet.Application.Common;
using FakeStoreNet.Infrastructure.Caching;

namespace FakeStoreNet.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register domain event infrastructure
            builder.Services.AddSingleton<FakeStoreNet.Domain.Common.IEventBus, FakeStoreNet.Infrastructure.EventDispatching.InMemoryEventBus>();
            builder.Services.AddSingleton<FakeStoreNet.Domain.Common.IEventLogRepository, FakeStoreNet.Infrastructure.EventDispatching.InMemoryEventLogRepository>();

            builder.Services.AddControllers();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
            builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection("CacheSettings"));

            var app = builder.Build();

            // Configure domain event subscriptions
            var eventBus = app.Services.GetRequiredService<FakeStoreNet.Domain.Common.IEventBus>();
            var eventLogRepo = app.Services.GetRequiredService<FakeStoreNet.Domain.Common.IEventLogRepository>();
            eventBus.Subscribe<FakeStoreNet.Domain.Common.Events.ProductCreatedEvent>(async evt =>
            {
                var payload = System.Text.Json.JsonSerializer.Serialize(evt);
                var log = new FakeStoreNet.Domain.Common.EventLog(evt.GetType().FullName!, payload, evt.OccurredOn);
                await eventLogRepo.SaveAsync(log);
            });

            app.UseHttpsRedirection();
            app.MapControllers();
            app.Run();
        }
    }
}

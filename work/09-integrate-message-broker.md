# 09. Integrate Message Broker for Event Publishing

## Description
Configure Azure Service Bus with Rebus for domain event publishing and consumption, enabling asynchronous integration between modules.  
Rationale: support decoupled, scalable, and resilient communication via Azure Service Bus and Rebus, allowing future extensions without direct coupling.

## Acceptance Criteria
- An Azure Service Bus namespace is configured and accessible.  
- The EventBus publisher uses Rebus with Azure Service Bus transport to send messages.  
- A sample Rebus consumer receives and processes messages from Azure Service Bus correctly.  
- Connection settings and credentials are parameterizable via `appsettings.json` or environment variables.  
- Integration tests verify the sending and consumption of at least one event type.

## Estimate
**Story Points:** 5 points

## Duration
**Junior Developer:** 45 hours
**Regular Developer:** 30 hours
**Senior Developer:** 20 hours
## Priority
High

## Assignee
—

## Dependencies
- 02. Implement Domain Event Dispatching

## Attachments & References
- docs/infrastructure/deployment/runbook.md  
- docs/tactical/modeling/domain-events.md

## Technical Refinement

- Broker Transport Configuration:
  - Use Rebus Azure Service Bus transport via DI:
    ```csharp
    services.AddRebus(config => config
      .Transport(t => t.UseAzureServiceBus(connectionString, "event-topic")));
    ```
  - Support subscription to topics and queues with `bus.Subscribe<TEvent>()`.

- Publisher Implementation:
  - Implement `IEventBus.PublishAsync<TEvent>(TEvent evt)` calling `bus.Publish(evt)`.
  - Ensure events are serialized to JSON with camelCase using `NewtonsoftJson` or `System.Text.Json` plugin.

- Consumer/Handler Setup:
  - Create handlers implementing `IHandleMessages<TEvent>`, registered via `services.AutoRegisterHandlers()`.
  - Configure error queue, retry strategy (e.g., immediate retries then delayed retries).

- Configuration & Secrets:
  - Load Service Bus connection string and entity names (topic, subscription) from `appsettings.json` or environment variables.
  - Use Azure Key Vault integration if required for secret retrieval.

- Testing Strategy:
  - Unit test handlers with in-memory Rebus transport or mocked `IBus`.
  - Integration tests against a live Service Bus namespace or emulator Docker container.
  - Validate message routing, retries, and dead-letter behavior.

This section provides detailed technical guidance for integrating Azure Service Bus with Rebus.

## Test Cases

```gherkin
Feature: Message Broker Integration

  Scenario: Publishing events to Azure Service Bus
    Given a ProductCreatedEvent occurs
    When EventBus.PublishAsync is called
    Then the message should be sent to Service Bus topic "event-topic"

  Scenario: Consuming messages from Azure Service Bus
    Given a message exists in Service Bus subscription
    When Rebus consumer handles the message
    Then the corresponding handler for the event should be invoked

  Scenario: Retry on transient messaging failures
    Given Azure Service Bus returns a transient error
    When Rebus attempts to publish
    Then it should retry according to configured retry policy
    And eventually succeed or move the message to dead-letter queue
```


# 02. Implement Domain Event Dispatching

## Description
Implement a mechanism for publishing and dispatching domain events after critical operations in the domain model (e.g., ProductCreatedEvent when creating a product).  
Rationale: facilitate internal communication and event-driven integration, enabling future extensions and asynchronous reactions without direct coupling.

## Acceptance Criteria
- When creating a Product instance, a ProductCreatedEvent is instantiated and enqueued.  
- Event infrastructure (EventBus) dispatches the event to at least one sample listener.  
- Sample listener persists an event log in an event repository.  
- Unit test coverage ≥ 80% for the event publishing and consumption flow.

## Estimate
5 points

## Priority
High

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects

## Attachments & References
- docs/tactical/modeling/domain-events.md

## Technical Refinement

- Domain Events:
  - Define event classes as immutable records with EventId (GUID), OccurredOn (UTC), AggregateId, and payload properties.
  - Implement a common IDomainEvent interface for type safety and discovery.

- Event Infrastructure:
  - Create IEventBus with asynchronous methods PublishAsync<TEvent>(TEvent evt) and Subscribe<TEvent>(Func<TEvent, Task> handler).
  - Support in-memory dispatch for testing and a pluggable transport for production (e.g., message broker).

- Dispatch Mechanism:
  - Integrate event collection in aggregates via a domain-events queue.
  - Publish collected events in UnitOfWork.CommitAsync() after database transaction succeeds.
  - Handle failures with retry policies and dead-letter logging.

- Event Logging:
  - Design an EventLog entity (Id, EventType, Payload as JSON, OccurredOn).
  - Use EF Core migrations to create the event log table with proper indices.
  - Serialize events to JSON using System.Text.Json with camelCase naming.

- Sample Listener:
  - Implement a ProductCreatedEventHandler subscribing to ProductCreatedEvent.
  - Persist event details to EventLogRepository and log success/failure.
  - Ensure idempotency by checking for existing EventLog entries before insert.

- Testing Strategy:
  - Write unit tests for event enqueuing, publishing, and handling flows.
  - Simulate failures in listener and verify retry and dead-letter behaviors.
- Achieve ≥80% coverage on the event dispatching module.

## Test Cases

```gherkin
Feature: Domain Event Dispatching

  Scenario: Enqueueing and publishing a ProductCreatedEvent
    Given a new Product has been created
    When CommitAsync is called on UnitOfWork
    Then a ProductCreatedEvent should be published to IEventBus
    And the EventLog should contain an entry for ProductCreatedEvent

  Scenario: Handling failures in event dispatch
    Given IEventBus.PublishAsync throws an exception
    When CommitAsync is invoked
    Then the system should retry publishing according to retry policy
    And after retries, failed events should be moved to dead-letter queue
```

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

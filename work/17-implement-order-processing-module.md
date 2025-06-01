# 17. Implement Order Processing Module

## Description
Develop a new domain module/Bounded Context “Orders” with entities, commands, queries, and integration with the core product and user modules.  
Rationale: separate responsibilities, organize order business logic, and lay the foundation for future payment integrations.

## Acceptance Criteria
- Order entity is created with properties (items, amounts, status, customer).  
- CQRS handlers for creating, updating, and querying orders are implemented.  
- Domain events (OrderCreatedEvent, OrderUpdatedEvent) are published.  
- Integration with EventBus and RabbitMQ for consuming product events (e.g., pre-existing stock requirement).  
- Unit and integration tests cover order processing flows.

## Estimate
5 points

## Priority
High

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects  
- 02. Implement Domain Event Dispatching  
- 03. Finalize CQRS Handlers Implementation

## Attachments & References
- docs/strategic-design/subdomain-classification.md  
- docs/tactical/modeling/domain-events.md  
- docs/infrastructure/api/openapi-orders.yaml

## Technical Refinement

- Domain Model:
  - Define `Order` aggregate with properties: `OrderId`, `CustomerId`, `List<OrderItem>`, `TotalAmount`, `Status`.
  - Create `OrderItem` value object with `ProductId`, `Quantity`, `UnitPrice`, and enforce `Quantity ≥ 1`.

- CQRS Handlers:
  - Implement `CreateOrderCommandHandler`, `UpdateOrderStatusCommandHandler`, and `GetOrderByIdQueryHandler`.
  - Use `UnitOfWork` to manage transactions and publish `OrderCreatedEvent` and `OrderUpdatedEvent` after commit.
  - Validate stock availability by subscribing to `ProductStockCheckedEvent` or synchronous check via repository.

- Domain Events:
  - Extend `OrderCreatedEvent` and `OrderUpdatedEvent` records with `OrderId`, `OccurredOn`, and payload properties.
  - Ensure events implement `IDomainEvent` and are published via `IEventBus`.

- Integration with Messaging:
  - Configure RabbitMQ transport in Rebus or MassTransit for `OrderCreatedEvent`.
  - Create a consumer for `ProductStockReservedEvent` to update order items or trigger compensation workflows.

- Persistence & Mappings:
  - Map `Order` aggregate in EF Core with owned `OrderItem` value objects.
  - Configure cascade delete behaviors and index `CustomerId` for query performance.

- Testing Strategy:
  - Unit test domain model invariants and command handlers using in-memory database and mocked event bus.
  - Integration tests with RabbitMQ test container to verify message publishing and consumption flows.
- Achieve ≥ 90% coverage on order processing module.

## Test Cases

```gherkin
Feature: Order Processing Module

  Scenario: Create new order successfully
    Given valid customer and product stock is sufficient
    When CreateOrderCommandHandler handles CreateOrderCommand
    Then an Order entity should be persisted with correct TotalAmount and Status "Pending"

  Scenario: Fail order creation due to insufficient stock
    Given product stock is 0
    When CreateOrderCommandHandler handles CreateOrderCommand
    Then a DomainValidationException should be thrown indicating "Insufficient stock"

  Scenario: Update order status flow
    Given an existing order with status "Pending"
    When UpdateOrderStatusCommandHandler handles UpdateOrderStatusCommand to "Confirmed"
    Then the order status should be updated to "Confirmed"

  Scenario: Retrieve order by id
    Given an order exists with OrderId 1
    When GetOrderByIdQueryHandler is executed with OrderId 1
    Then the returned OrderDto should match the persisted order data
```

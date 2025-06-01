# 03. Finalize CQRS Handlers Implementation

## Description
Complete the implementation of all CQRS handlers (commands and queries) in the application layer, ensuring each write and read operation is handled correctly.  
Rationale: enable clear separation of concerns and improve maintainability and testability of domain operations.

## Acceptance Criteria
- All commands (CreateProduct, UpdateProduct, DeleteProduct, etc.) have handlers that perform proper validation and persistence.  
- All queries (GetProductById, GetAllProducts, etc.) have handlers that return correct DTOs.  
- Unit tests cover the flow of each handler with both positive and negative scenarios.  
- Documentation in the application layer is updated to reflect request ↔ handler mappings.

## Estimate
5 points

## Priority
High

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects  
- 02. Implement Domain Event Dispatching

## Attachments & References
- docs/tactical/modeling/repository-interfaces.md  
- docs/infrastructure/api/openapi-customers.yaml

## Technical Refinement

- **Command Handlers**:
  - Validate command inputs using FluentValidation or equivalent.
  - Map command DTOs to domain entities with AutoMapper or manual mapping.
  - Use UnitOfWork to wrap persistence operations and commit transactions on success.
  - Ensure atomicity and rollback on exceptions.

- **Query Handlers**:
  - Optimize EF Core queries with Select projections directly to DTOs.
  - Implement pagination and filtering parameters for list endpoints.
  - Handle not-found scenarios by returning null or throwing domain exceptions.

- **Error Handling & Logging**:
  - Throw domain-specific exceptions on validation or business rule failures.
  - Log handler execution and exceptions using ILogger with structured logs.
  - Use middleware to translate exceptions into appropriate HTTP responses.

- **Testing Strategy**:
  - Write unit tests with xUnit and Moq for both happy path and failure cases.
  - Create integration tests to validate end-to-end request-to-handler flows, including database interactions.
  - Achieve ≥ 90% coverage on application layer handlers.

This section outlines the detailed technical expectations for a robust and maintainable CQRS handler implementation.

## Test Cases

```gherkin
Feature: CQRS Handler Operations

  Scenario: Successfully handling CreateProduct command
    Given a CreateProductCommand with valid product data
    When the CreateProductCommandHandler handles the command
    Then a new Product entity should be persisted
    And the database should contain Product with matching properties

  Scenario: Handling CreateProduct command with invalid data
    Given a CreateProductCommand with missing required fields
    When the CreateProductCommandHandler handles the command
    Then a DomainValidationException should be thrown

  Scenario: Successfully executing GetProductById query
    Given a product exists with Id 1
    When GetProductByIdQueryHandler is executed with Id 1
    Then the handler returns a ProductDto with matching Id and properties

  Scenario: Query handler returns not found
    Given no product exists with Id 999
    When GetProductByIdQueryHandler is executed with Id 999
    Then a NotFoundException should be thrown or null result returned
```

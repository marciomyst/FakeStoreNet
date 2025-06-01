# 05. Refine Domain Validation & Invariants

## Description
Enhance validation rules across all entities and value objects, ensuring business invariants are consistently enforced.  
Rationale: prevent invalid states in the domain, improving reliability and reducing production errors.

## Acceptance Criteria
- FluentValidation or native validations are implemented for all relevant commands.  
- Invariants (e.g., price ≥ 0, stock ≥ 0, valid email) are verified before persistence.  
- Standardized and informative error feedback is provided for rule violations.  
- Unit tests cover both positive and negative validation scenarios for all entities.

## Estimate
**Story Points:** 3 points

## Duration
**Junior Developer:** 27 hours
**Regular Developer:** 18 hours
**Senior Developer:** 12 hours
## Priority
High

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects

## Attachments & References
- docs/tactical/modeling/business-rules.md  
- docs/infrastructure/api/openapi-customers.yaml

## Technical Refinement

- Validation Framework:
  - Define FluentValidation validators for each command DTO and value object.
  - Integrate a validation pipeline (e.g., MediatR behaviour) to automatically trigger validation before handler execution.
  - Centralize common validation rules (e.g., email format, numeric bounds) in reusable validator base classes.

- Domain Invariants Enforcement:
  - Enforce invariants at the entity and value object constructor level using guard clauses.
  - Throw domain-specific exceptions (e.g., DomainValidationException) with clear error codes and messages.
  - Use `Ensure.That` or custom guard utility for concise invariant checks.

- Error Feedback & API Integration:
  - Aggregate validation failures into `ValidationProblemDetails` for API responses.
  - Map exceptions to standardized HTTP status codes (e.g., 400 for validation errors).
  - Include field-level error metadata for client-side resolution.

- Testing Strategy:
  - Write unit tests for all validators, covering positive, negative, and boundary cases.
  - Create integration tests to verify API endpoints return proper validation responses.
- Achieve ≥ 90% coverage on validation and invariant enforcement logic.

## Test Cases

```gherkin
Feature: Domain Validation and Invariants

  Scenario: Prevent negative price
    Given a command with price -10
    When the validation pipeline runs
    Then a DomainValidationException should be thrown with message "Price must be ≥ 0"

  Scenario: Prevent negative stock quantity
    Given a Quantity value object with quantity -5
    When the constructor is invoked
    Then a DomainValidationException should be thrown with message "Quantity must be ≥ 0"

  Scenario: Validate email format on user creation
    Given a CreateUserCommand with email "invalid-email"
    When handler validation executes
    Then a DomainValidationException should be thrown indicating "Email format is invalid"

  Scenario: Accept valid inputs without errors
    Given price 20, quantity 5, and email "user@example.com"
    When entities and value objects are created
    Then no exceptions should be thrown
```


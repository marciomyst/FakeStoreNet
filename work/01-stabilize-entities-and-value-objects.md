# 01. Stabilize Entities & Value Objects

## Description
Stabilize and refine the domain model by clearly defining entities and value objects along with their invariants and business rules.  
Rationale: ensure domain consistency and integrity, providing a reliable foundation for all higher layers.

## Acceptance Criteria
- All primary entities (Product, User, Order, Cart) have clear property and behavior definitions.
- Value objects (e.g., Money, Address, Quantity) implement invariant validations.
- Critical business rules (e.g., price ≥ 0, quantity ≥ 1) are automatically enforced in constructors.
- Unit test coverage ≥ 90% for these classes.

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
—

## Attachments & References
- docs/strategic-design/subdomain-classification.md  
- docs/tactical/modeling/value-objects.md

## Technical Refinement

- Entities:
  - Define behavior methods for Product, User, Order, and Cart (e.g., AdjustPrice, SubmitOrder).
  - Use private/protected setters and constructor validation to enforce invariants.
  - Override equality members (Equals, GetHashCode) for consistent identity handling.

- Value Objects:
  - Implement validation logic in constructors for Money, Address, and Quantity.
  - Provide factory methods and conversions (e.g., currency conversion, address formatting).
  - Ensure immutability by making fields readonly and not exposing setters.

- Business Rules Enforcement:
  - Enforce price ≥ 0 on creation and updates.
  - Ensure quantity ≥ 1 and does not exceed available stock.
  - Validate address includes mandatory fields (street, city, postal code, country).

- Persistence Mapping:
  - Configure EF Core to map aggregates, using owned entity types for value objects.
  - Define correct foreign key relationships and cascade behaviors.

- Testing Strategy:
  - Write unit tests covering both valid and invalid scenarios for each invariant.
  - Target >90% coverage on domain classes, including exception cases.

This section outlines the detailed technical expectations to build a robust and consistent domain model.

## Test Cases

```gherkin
Feature: Domain Model Invariants

  Scenario: Creating a Money value object with valid amount and currency
    Given amount is 100 and currency is "USD"
    When a Money object is created
    Then the Money object should be created without errors
    And its Amount property should be 100
    And its Currency property should be "USD"

  Scenario: Prevent creating a Money value object with negative amount
    Given amount is -50 and currency is "USD"
    When a Money object is created
    Then a DomainValidationException should be thrown with message "Amount must be ≥ 0"

  Scenario: Creating an Address value object with all required fields
    Given street is "123 Main St", city is "Springfield", postal code is "12345", country is "US"
    When an Address object is created
    Then the Address object should be created without errors

  Scenario: Creating an Address value object missing mandatory field
    Given street is "" and city is "Springfield", postal code is "12345", country is "US"
    When an Address object is created
    Then a DomainValidationException should be thrown indicating "Street is required"
```


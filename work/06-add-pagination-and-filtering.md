# 06. Add Pagination & Filtering

## Description
Extend `GetAllProductsQuery` to support pagination parameters (`page`, `pageSize`) and filters (e.g., category, price range, search term).  
Rationale: enable efficient retrieval of product subsets, improve user experience, and reduce load on endpoints.

## Acceptance Criteria
- Endpoint accepts `page` (≥1) and `pageSize` (max. 100) parameters and uses them to limit results.  
- Filters for category, price range, and search term (name/description) can be applied together.  
- Response includes pagination metadata (`totalItems`, `totalPages`, `currentPage`, `pageSize`).  
- Integration tests validate pagination and filtering behavior correctly.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 03. Finalize CQRS Handlers Implementation

## Attachments & References
- docs/infrastructure/api/openapi-orders.yaml  
- docs/infrastructure/observability.md

## Technical Refinement

- Parameter Validation:
  - Ensure `page` ≥ 1 and `pageSize` between 1 and 100 using guard clauses or FluentValidation.
  - Validate filter values (e.g., price range min ≤ max, category exists).

- Query Handler Implementation:
  - Extend `GetAllProductsQuery` with `Page` and `PageSize` properties, plus filter criteria.
  - Use EF Core’s `Skip((page - 1) * pageSize).Take(pageSize)` and `Where` clauses for filters.
  - Apply dynamic expressions or `IQueryable` extensions for combining filters.

- Pagination Metadata:
  - Query total count for matching filters and compute `TotalPages = Ceil(totalCount / pageSize)`.
  - Include `TotalItems`, `TotalPages`, `CurrentPage`, and `PageSize` in the response DTO.

- Performance & Indexing:
  - Add database indexes on filterable columns (category, price, name).
  - Use asynchronous query execution (`ToListAsync`, `CountAsync`) for scalability.

- Testing Strategy:
  - Unit tests with in-memory DB context to verify pagination logic and filter combinations.
  - Integration tests against a test database to confirm metadata and result correctness.

This section details technical requirements for implementing robust pagination and filtering in query handlers.

## Test Cases

```gherkin
Feature: Pagination and Filtering

  Scenario: Retrieve first page of products
    Given there are 50 products in the database
    When GetAllProductsQuery is executed with page=1 and pageSize=10
    Then 10 products should be returned
    And response metadata should indicate totalItems=50 and totalPages=5

  Scenario: Prevent invalid page parameters
    Given pageSize is 200
    When GetAllProductsQuery is executed
    Then a validation error should occur indicating "pageSize must be between 1 and 100"

  Scenario: Apply category and price filters
    Given products exist in category "Electronics" with prices between 100 and 200
    When GetAllProductsQuery is executed with filter category="Electronics", minPrice=100, maxPrice=200
    Then only products matching those filters should be returned
```

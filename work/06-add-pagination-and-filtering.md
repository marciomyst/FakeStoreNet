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

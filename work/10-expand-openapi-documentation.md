# 10. Expand OpenAPI Documentation for All Endpoints

## Description
Review and enrich the OpenAPI specification to ensure that all current and future endpoints are documented with parameter details, request/response examples, and status codes.  
Rationale: improve communication with external teams, facilitate API consumption, and enable tooling support (Swagger UI, generated clients).

## Acceptance Criteria
- OpenAPI files (`.yaml`) are updated to cover all existing endpoints.  
- Valid request and response payload examples are provided for each operation.  
- Status codes and possible error responses are documented for every route.  
- Swagger UI or ReDoc is configured to reflect the current specification.  
- Automated contract tests validate compliance between implementation and specification.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 03. Finalize CQRS Handlers Implementation

## Attachments & References
- docs/infrastructure/api/openapi-customers.yaml  
- docs/infrastructure/api/openapi-orders.yaml  
- docs/infrastructure/api/openapi-products.yaml (new)

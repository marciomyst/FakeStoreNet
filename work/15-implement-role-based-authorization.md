# 15. Implement Role-Based Authorization

## Description
Add role-based access control to API endpoints by defining authorization policies and mapping permissions according to user roles.  
Rationale: ensure security and separation of responsibilities, allowing only authorized users to perform sensitive operations.

## Acceptance Criteria
- Roles (e.g., Admin, Customer, Guest) are defined in the identity system.  
- Authorization policies are configured in ASP.NET Core (e.g., `[Authorize(Roles = "Admin")]`).  
- Sensitive endpoints (create, update, delete products and orders) are protected with appropriate roles.  
- HTTP 403 is returned when an authenticated user lacks permission.  
- Integration tests validate allowed and denied access based on roles.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 03. Finalize CQRS Handlers Implementation  
- 10. Expand OpenAPI Documentation for All Endpoints

## Attachments & References
- docs/infrastructure/api/openapi-customers.yaml  
- docs/infrastructure/api/openapi-orders.yaml  
- docs/infrastructure/api/openapi-products.yaml

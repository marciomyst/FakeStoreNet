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

## Technical Refinement

- Role Definitions & Policies:
  - Define roles (Admin, Customer, Guest) as constants or enum and include in JWT claims.
  - Configure ASP.NET Core authorization policies via `services.AddAuthorization(options => { options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin")); });`.

- Endpoint Protection:
  - Apply `[Authorize(Roles = "Admin")]` or `[Authorize(Policy = "RequireAdmin")]` attributes on controllers/actions.
  - Use policy-based requirements for more granular permissions (e.g., CRUD operations).

- OpenAPI Security Integration:
  - Update OpenAPI YAML to include `securitySchemes` for Bearer token.
  - Add `security` section to protected paths specifying required roles via `x-roles` vendor extension or descriptions.

- Error Handling:
  - Ensure unauthorized requests return HTTP 401 and forbidden return HTTP 403 with problem details.
  - Use exception handling middleware to convert `AuthorizationFailure` to proper API response.

- Testing Strategy:
  - Write integration tests using `WebApplicationFactory` with pre-generated JWTs for each role.
  - Validate that Admin tokens succeed on protected routes and Customer/Guest tokens receive 403.
  - Automate token creation using test fixtures and mock identity provider if needed.
This section details the steps for robust role-based authorization and security integration.

## Test Cases

```gherkin
Feature: Role-Based Authorization

  Scenario: Admin role can access protected endpoint
    Given a user with role "Admin" and valid JWT token
    When the user sends a POST request to /api/products
    Then the response status should be 201 Created

  Scenario: Customer role is forbidden from deleting products
    Given a user with role "Customer" and valid JWT token
    When the user sends a DELETE request to /api/products/1
    Then the response status should be 403 Forbidden

  Scenario: Unauthenticated request returns 401
    Given no authentication token is provided
    When the user sends a GET request to /api/orders
    Then the response status should be 401 Unauthorized
```

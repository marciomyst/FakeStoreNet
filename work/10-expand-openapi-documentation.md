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

## Technical Refinement

- Component Schemas:
  - Consolidate common request/response models under `components.schemas` (e.g., ProductDto, OrderDto).
  - Use `$ref` references to avoid duplication across paths.

- Path Parameters & Headers:
  - Define reusable parameters in `components.parameters` for pagination, filtering, and IDs.
  - Standardize header definitions (e.g., `Authorization`) in `components.headers`.

- Examples & Media Types:
  - Add realistic `examples` for request and response bodies under `components.examples`.
  - Provide multiple media type examples (e.g., `application/json`).

- Security Schemes:
  - Declare `components.securitySchemes` for BearerToken and APIKey auth.
  - Apply security requirements to protected operations via `security` section.

- Swagger UI & ReDoc Configuration:
  - Configure `AddSwaggerGen` with `IncludeXmlComments` and custom `SwaggerDoc` metadata.
  - Enable `SwaggerUIOptions` to serve at `/swagger` and ReDoc middleware at `/redoc`.

- Automated Contract Tests:
  - Use ASP.NET Core `WebApplicationFactory` to serve the API and generate live OpenAPI spec.
  - Integrate spec validation in tests (e.g., `AssertOpenApiSpec` or `NSwagStudio`) to compare against YAML files.
  - Fail CI pipeline if discrepancies are detected.

This section outlines the detailed enhancements required to maintain a comprehensive, accurate, and testable OpenAPI documentation suite.

## Test Cases

```gherkin
Feature: OpenAPI Documentation Validation

  Scenario: Ensure all endpoints are documented
    Given the live API is running
    When the OpenAPI spec is retrieved from `/swagger/v1/swagger.json`
    Then every route returned by the API should appear under `paths` in the spec

  Scenario: Validate request/response examples
    Given the OpenAPI spec includes examples for POST /products
    When the spec is used to generate a client request
    Then the example body should be accepted by the API without errors

  Scenario: Contract tests for status codes
    Given the API returns a 400 on invalid input for POST /orders
    When the OpenAPI spec lists 400 as a possible response
    Then automated contract tests should pass for that error scenario
```

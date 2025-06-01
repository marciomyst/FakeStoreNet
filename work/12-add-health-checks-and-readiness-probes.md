# 12. Add Health Checks & Readiness Probes

## Description
Implement health check and readiness probe endpoints to monitor application availability and component statuses (database, cache, broker).  
Rationale: ensure orchestrators and observability systems can detect health state and prevent traffic to non-ready instances.

## Acceptance Criteria
- `/health/live` and `/health/ready` endpoints are available and return appropriate HTTP status codes.  
- Health check verifies connectivity with the database, cache, and broker.  
- Readiness probe returns “ready” only when all components are operational.  
- Documentation for these endpoints is added to the OpenAPI specification.  
- Integration tests validate the behavior of the health endpoints.

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
- 07. Add PostgreSQL Provider Support  
- 08. Implement Database Migrations

## Attachments & References
- docs/infrastructure/observability.md  
- docs/infrastructure/api/openapi-orders.yaml

## Technical Refinement

- Health Checks Registration:
  - Use `services.AddHealthChecks()` in `Startup`/`Program` to register checks:
    - `.AddDbContextCheck<YourDbContext>("Database")`
    - `.AddCheck<CacheHealthCheck>("Cache")`
    - `.AddCheck<BrokerHealthCheck>("MessageBroker")`
  - Tag readiness checks with `"ready"` and liveness checks with `"live"`.

- Endpoint Mapping:
  - Map `/health/live` for liveness: 
    ```csharp
    app.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false });
    ```
  - Map `/health/ready` for readiness:
    ```csharp
    app.MapHealthChecks("/health/ready", new HealthCheckOptions { Predicate = hc => hc.Tags.Contains("ready") });
    ```
  - Configure `ResponseWriter` to output detailed JSON.

- OpenAPI Documentation:
  - Add `/health/live` and `/health/ready` paths to the Swagger YAML spec.
  - Include response schemas for health check results under `components.schemas.HealthReport`.

- Testing Strategy:
  - Write integration tests using `WebApplicationFactory` to call both endpoints.
  - Simulate service failures in tests to verify correct HTTP status codes (200 vs 503).
  - Ensure health checks reflect actual component states.

This section details the technical steps to implement robust health and readiness probes in the application.

## Test Cases

```gherkin
Feature: Health Checks and Readiness Probes

  Scenario: Liveness endpoint returns 200 when application is running
    Given the application is started
    When a GET request is made to `/health/live`
    Then the response status should be 200
    And the response body should indicate liveness OK

  Scenario: Readiness endpoint returns 503 when dependencies are down
    Given the database is unreachable
    When a GET request is made to `/health/ready`
    Then the response status should be 503
    And the response body should include "Database" in the failing checks

  Scenario: Readiness endpoint returns 200 when all components are healthy
    Given the database, cache, and broker are operational
    When a GET request is made to `/health/ready`
    Then the response status should be 200
    And the response body should indicate readiness OK
```


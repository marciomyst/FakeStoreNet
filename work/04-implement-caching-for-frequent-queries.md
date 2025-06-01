# 04. Implement Caching for Frequent Queries

## Description
Add a caching layer for results of frequently accessed queries (e.g., GetAllProductsQuery), reducing latency and database load.  
Rationale: improve performance and scalability by providing faster responses to users and alleviating repetitive querying.

## Acceptance Criteria
- Defined query responses are stored in cache (e.g., MemoryCache or Redis) with configurable expiration times.  
- Cache invalidation is implemented when underlying data changes (e.g., create, update, delete product).  
- Cache hit/miss metrics are exposed for monitoring.  
- Unit and integration tests validate caching behavior.

## Estimate
3 points

## Priority
Medium

## Assignee
—

## Dependencies
- 03. Finalize CQRS Handlers Implementation

## Attachments & References
- docs/infrastructure/observability.md  
- docs/infrastructure/api/openapi-orders.yaml

## Technical Refinement

- Caching Infrastructure:
  - Define `ICacheService` with methods `GetAsync<T>`, `SetAsync<T>`, and `RemoveAsync`.
  - Implement in-memory (`IMemoryCache`) and distributed (`IDistributedCache`/Redis) providers, configured via DI.
  - Register caching services in `Startup`/`Program` using `AddMemoryCache` and `AddStackExchangeRedisCache`.

- Cache Keys and Policies:
  - Standardize key naming: `<QueryName>:<ParameterHash>`; provide a helper for complex queries.
  - Configure absolute and sliding expiration per query via `CacheSettings` in `appsettings.json`.
  - Enforce key length limits for Redis (≤ 256 characters).

- Invalidation Strategies:
  - Inject `ICacheService` into write-side handlers to evict or refresh keys after create/update/delete operations.
  - Optionally publish domain events (e.g., `ProductUpdatedEvent`) and subscribe to invalidate caches asynchronously.

- Metrics and Monitoring:
  - Decorate cache calls to emit hit/miss counters and latency histograms (e.g., Prometheus, Application Insights).
  - Tag metrics with query names and result status.

- Testing Strategy:
  - Unit test `CacheService` implementations using in-memory providers and mocked `IDistributedCache`.
  - Integration tests against a real Redis instance (use Docker Compose) to validate TTL and eviction behavior.
  - Achieve ≥ 90% coverage on caching-related code paths.

This section provides detailed technical guidance to ensure a performant, configurable, and observable caching layer for frequent queries.

## Test Cases

```gherkin
Feature: Caching for Frequent Queries

  Scenario: Cache hit on repeated GetAllProductsQuery
    Given the cache contains a response for GetAllProductsQuery with parameters page=1, pageSize=10
    When GetAllProductsQueryHandler is executed with page=1, pageSize=10
    Then the response should be served from cache
    And no database query should be executed

  Scenario: Cache miss and population for GetAllProductsQuery
    Given the cache does not contain a response for GetAllProductsQuery with parameters page=2, pageSize=20
    When GetAllProductsQueryHandler is executed with page=2, pageSize=20
    Then the handler should query the database
    And the response should be stored in cache with correct key and expiration

  Scenario: Cache invalidation on product update
    Given a product is updated via UpdateProductCommandHandler
    When UpdateProductCommandHandler completes successfully
    Then cache entries related to GetAllProductsQuery should be evicted

  Scenario: Cache metrics reporting
    Given the caching layer records hit and miss metrics
    When GetAllProductsQueryHandler processes a request
    Then a hit or miss metric should be emitted with appropriate tag values
```

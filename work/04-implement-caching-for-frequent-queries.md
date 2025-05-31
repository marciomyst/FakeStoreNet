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

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
3 points

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

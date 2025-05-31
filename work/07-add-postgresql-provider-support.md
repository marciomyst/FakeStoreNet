# 07. Add PostgreSQL Provider Support

## Description
Implement support for the PostgreSQL database in the infrastructure layer, allowing flexible configuration via connection strings.  
Rationale: provide a robust and widely adopted production alternative, ensuring performance and scalability.

## Acceptance Criteria
- PostgreSQL provider configuration is available in `appsettings.json` and environment variables.  
- `DbContext` uses the correct provider based on configuration (InMemory, SQLite, or PostgreSQL).  
- EF Core–generated SQL scripts execute without errors in PostgreSQL.  
- Integration tests validate basic CRUD operations against a PostgreSQL instance (e.g., using a Docker container).

## Estimate
5 points

## Priority
High

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects

## Attachments & References
- docs/infrastructure/deployment/docker-compose.yml  
- docs/infrastructure/migrations/

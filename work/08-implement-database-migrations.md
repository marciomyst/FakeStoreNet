# 08. Implement Database Migrations

## Description
Configure and implement a database schema migration mechanism using EF Core Migrations (or Flyway if needed), ensuring change history and controlled versioning.  
Rationale: enable safe database evolution, schema version control across different environments, and deployment automation.

## Acceptance Criteria
- Project is configured to create and apply migrations via CLI (`dotnet ef migrations add`, `dotnet ef database update`).  
- Migrations folder is organized and versioned in the repository.  
- Migrations can be executed automatically in the CI/CD pipeline.  
- Integration tests verify that the schema created via migrations supports all CRUD operations.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 07. Add PostgreSQL Provider Support

## Attachments & References
- docs/infrastructure/migrations/  
- docs/infrastructure/deployment/runbook.md

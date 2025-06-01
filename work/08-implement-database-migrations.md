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

## Attachments & References
- docs/infrastructure/migrations/  
- docs/infrastructure/deployment/runbook.md

## Technical Refinement

- Migrations Folder Organization:
  - Use a dedicated `Migrations/` folder per DbContext with timestamped migration files (YYYYMMDD_HHMMSS_MigrationName).
  - Include both `Up` and `Down` methods for reversible schema changes.

- CLI Commands & Scripts:
  - Standardize on `dotnet ef migrations add <Name>` and `dotnet ef database update` in project root.
  - Provide PowerShell/Bash scripts (e.g., `apply-migrations.ps1`) that wrap EF commands and handle environment variables.

- CI/CD Automation:
  - Integrate migration application into the deployment pipeline (e.g., before running application in staging/production).
  - Fail the build if migrations do not compile or if pending migrations exist.

- Rollback & Version Control:
  - Enforce code review of migration files to ensure safe schema changes.
  - Support rolling back to a specific migration using `dotnet ef database update <MigrationId>`.

- Testing Strategy:
  - Use a Docker-based test database and run migrations at test startup.
  - Write integration tests to verify that the database schema matches the EF Core model.
  - Use snapshots or reflection to validate that all expected tables and columns exist.

This section outlines detailed practices for managing EF Core migrations reliably and safely across environments.

## Test Cases

```gherkin
Feature: Database Migrations

  Scenario: Adding and applying initial migration
    Given no migrations exist in the project
    When running `dotnet ef migrations add InitialCreate`
    Then an InitialCreate migration file should be created
    And running `dotnet ef database update` should apply schema without errors

  Scenario: Rolling back to a specific migration
    Given multiple migrations exist including InitialCreate and AddOrders
    When running `dotnet ef database update InitialCreate`
    Then the database schema should revert to the state after InitialCreate migration

  Scenario: CI pipeline applies migrations
    Given the CI pipeline is configured to run migrations
    When the migration step executes
    Then migrations should apply successfully and the job should succeed
    And migration logs should be captured as artifacts

  Scenario: CI pipeline fails on pending migrations
    Given pending migrations exist
    When the CI pipeline checks for pending migrations
    Then the pipeline should fail with an error indicating pending migrations
```


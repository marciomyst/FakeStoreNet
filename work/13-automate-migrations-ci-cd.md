# 13. Automate Database Migrations in CI/CD

## Description
Configure the CI/CD pipeline to automatically apply database migrations in test and staging environments, ensuring schema synchronization before code deployment.  
Rationale: reduce manual errors, accelerate delivery, and maintain environment consistency.

## Acceptance Criteria
- The pipeline runs `dotnet ef database update` after build in configured environments.  
- Connection variables are parameterized per environment (dev, staging, prod).  
- The pipeline fails if migrations do not apply successfully.  
- Migration execution logs are captured and stored.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 08. Implement Database Migrations

## Attachments & References
- docs/infrastructure/deployment/runbook.md  
- .github/workflows/ci.yml (new)

## Technical Refinement

- CI/CD Pipeline Steps:
  - Add stages in `.github/workflows/ci.yml` after build to install EF tooling and run:
    ```yaml
    - name: Apply EF Core Migrations
      run: dotnet ef database update --connection "$DB_CONN_STRING"
      env:
        DB_CONN_STRING: ${{ secrets.DB_CONN_STRING }}
    ```
  - Ensure the `ef` tool is available via `dotnet tool install --global dotnet-ef` if needed.
  - Use idempotent migration scripts in cases where multiple environments share history:
    ```bash
    dotnet ef migrations script --idempotent -o migrations.sql
    ```

- Error Handling & Artifacts:
  - Redirect migration command output to a file and upload as workflow artifact for diagnostics:
    ```yaml
    - name: Save Migration Logs
      run: |
        dotnet ef database update > migration.log 2>&1
      continue-on-error: false
    - uses: actions/upload-artifact@v2
      with:
        name: migration-log
        path: migration.log
    ```
  - Fail the job on any non-zero exit code from migration commands.

- Matrix Strategy:
  - Use GitHub Actions matrix to target multiple environments (e.g., dev, staging) with different connection strings.

- Smoke Tests & Validation:
  - After migrations, run a short script or test to verify key tables exist:
    ```yaml
    - name: Smoke Test Schema
      run: dotnet run --project tests/SmokeTests -- --connection "$DB_CONN_STRING"
    ```

- Rollback & Runbook:
  - Provide a workflow step for rollback:
    ```yaml
    - name: Rollback to Specific Migration
      run: dotnet ef database update 20230501_InitialPostgres --connection "$DB_CONN_STRING"
    ```
  - Document rollback commands and parameters in `docs/infrastructure/deployment/runbook.md`.

This section defines how to automate, validate, and manage database migrations in the CI/CD pipeline.

## Test Cases

```gherkin
Feature: CI/CD Database Migrations

  Scenario: Pipeline applies migrations successfully
    Given pending EF migrations exist
    When the CI workflow runs the Apply EF Core Migrations step
    Then `dotnet ef database update` should succeed with exit code 0

  Scenario: Pipeline captures migration logs
    Given migrations are applied in CI
    When migration logs are generated
    Then migration.log artifact should be uploaded

  Scenario: Pipeline fails on migration error
    Given an invalid migration script
    When migrations step is executed
    Then the CI job should fail and show error output
```

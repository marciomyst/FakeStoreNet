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

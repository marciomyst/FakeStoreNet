# 18. Extract Generic Domain as NuGet Package

## Description
Isolate reusable, generic domain components into a separate class library project, package it as a NuGet library, and publish it to an internal feed.  
Rationale: promote reuse across multiple projects, simplify maintenance, and enable versioning of generic domain logic.

## Acceptance Criteria
- A class library project (.NET Standard or .NET 6) is created containing:  
  - Generic entities (e.g., BaseEntity, EntityWithId)  
  - Common value objects (e.g., Money, GenericAddress)  
  - Shared interfaces (e.g., IRepository\<T\>)  
- The build process produces a `.nupkg` package that can be consumed.  
- Private NuGet feed configuration supports automatic publishing via CI pipeline.  
- The FakeStoreNet project is updated to reference the generic domain package.  
- Unit tests for the package achieve ≥ 90% coverage.

## Estimate
5 points

## Priority
Medium

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects  
- 03. Finalize CQRS Handlers Implementation

## Attachments & References
- docs/strategic-design/subdomain-classification.md  
- docs/infrastructure/deployment/runbook.md

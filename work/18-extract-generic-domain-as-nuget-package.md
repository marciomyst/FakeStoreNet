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
**Story Points:** 5 points

## Duration
**Junior Developer:** 45 hours
**Regular Developer:** 30 hours
**Senior Developer:** 20 hours
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

## Technical Refinement

- Class Library Project:
  - Create `Domain.Common` class library targeting .NET Standard 2.1 or .NET 6.
  - Move generic entities (BaseEntity, EntityWithId), value objects (Money, GenericAddress), and interfaces (IRepository<T>) into `Domain.Common` namespace.

- Packaging Configuration:
  - Add `<GeneratePackageOnBuild>true</GeneratePackageOnBuild>` and metadata (`PackageId`, `Version`, `Authors`, `Description`) to `Domain.Common.csproj`.
  - Configure package dependencies and target frameworks via `<ItemGroup>` in the csproj.

- NuGet Feed & CI Publishing:
  - Add private feed URL to `nuget.config` and authenticate via environment variables or secrets.
  - In CI pipeline (`.github/workflows/ci.yml`), add steps:
    ```yaml
    - name: Pack Domain.Common
      run: dotnet pack Domain.Common/Domain.Common.csproj --configuration Release --output ./artifacts
    - name: Push Package
      run: dotnet nuget push ./artifacts/*.nupkg --source ${{ secrets.NUGET_FEED_URL }}
    ```
  
- Consuming Package:
  - Remove local generic domain code from FakeStoreNet and add:
    ```xml
    <PackageReference Include="Domain.Common" Version="x.y.z" />
    ```
    to `FakeStoreNet.csproj`.
  - Ensure version range and asset exclusions are configured as needed.

- Testing & Quality:
  - Create `Domain.Common.Tests` project with xUnit and Moq to cover all public APIs.
  - Integrate `coverlet.collector` and `reportgenerator` to enforce ≥90% coverage in CI.

- Versioning & Releases:
  - Adopt semantic versioning for package versions.
  - Tag Git commits for each release and generate release notes using `git tag` annotations.

This section details steps to extract, package, publish, and consume a generic domain library as a NuGet package.

## Test Cases

```gherkin
Feature: Generic Domain NuGet Package

  Scenario: Build and pack Domain.Common library
    Given the Domain.Common project is configured with GeneratePackageOnBuild
    When the project is built in Release mode
    Then a .nupkg package should be created in the artifacts folder

  Scenario: Push package to private feed
    Given a valid NUGET_FEED_URL secret
    When dotnet nuget push is executed
    Then the package should be available in the private feed

  Scenario: Consume package in FakeStoreNet project
    Given the Domain.Common package version x.y.z is published
    When FakeStoreNet references Domain.Common via PackageReference
    Then the build should succeed and types from Domain.Common should be resolvable

  Scenario: Unit tests cover package public APIs
    Given Domain.Common.Tests project is configured with coverlet
    When tests are executed
    Then coverage report should show ≥90% coverage for Domain.Common
```


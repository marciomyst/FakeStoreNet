# 11. Increase Unit Test Coverage

## Description
Raise unit test coverage in the Domain and Application layers to at least 100%, ensuring all business rules and key flows are validated in isolation.  
Rationale: reduce regressions, facilitate future refactoring, and increase confidence in the codebase.

## Acceptance Criteria
- Coverage report shows 100% coverage in Domain and Application.  
- All validation scenarios, commands, queries, and events have corresponding tests.  
- Coverage tool (e.g., coverlet) is integrated into CI to generate automated reports.  
- Builds fail if the minimum coverage threshold is not met.

## Estimate
**Story Points:** 5 points

## Duration
**Junior Developer:** 45 hours
**Regular Developer:** 30 hours
**Senior Developer:** 20 hours
## Priority
High

## Assignee
—

## Dependencies
- 03. Finalize CQRS Handlers Implementation  
- 05. Refine Domain Validation & Invariants

## Attachments & References
- docs/infrastructure/testing-strategy.md

## Technical Refinement

- Coverage Tools:
  - Integrate `coverlet.collector` and `reportgenerator` in test projects to produce detailed reports.
  - Configure thresholds in project files for 100% coverage using `<Threshold>100</Threshold>` and output formats (`opencover`, `lcov`).

- Test Organization:
  - Create separate test projects `Domain.Tests` and `Application.Tests` with clear naming conventions.
  - Use xUnit collection fixtures and parallelization settings to manage shared context.

- Mocking & Isolation:
  - Use `Moq` or `NSubstitute` to mock repositories and external services.
  - Ensure no external resource calls in unit tests by isolating dependencies.

- Continuous Integration:
  - Run `dotnet test --collect:"XPlat Code Coverage"` in CI pipelines.
  - Use reportgenerator quality gates to fail builds when coverage < 100%.

- Reporting & Analysis:
  - Publish coverage reports as CI artifacts and integrate with Codecov or SonarQube for visualization.
  - Automate badge updates in README to reflect current coverage status.

This section outlines the detailed steps to achieve and enforce 100% unit test coverage in Domain and Application layers.

## Test Cases

```gherkin
Feature: Enforce Unit Test Coverage Threshold

  Scenario: Build fails when coverage falls below threshold
    Given unit test coverage for Domain layer is 99%
    And unit test coverage for Application layer is 100%
    When CI pipeline runs with coverage threshold set to 100%
    Then the build should fail indicating "Coverage threshold not met"

  Scenario: Build succeeds when coverage meets threshold
    Given unit test coverage for Domain and Application layers is 100%
    When CI pipeline runs with coverage threshold set to 100%
    Then the build should succeed

  Scenario: Coverage report published as artifact
    Given tests have run and coverage data is generated
    When CI pipeline completes test step
    Then the coverage report should be uploaded as a CI artifact
```


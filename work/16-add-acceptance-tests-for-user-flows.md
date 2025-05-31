# 16. Add Acceptance Tests for User Flows

## Description
Develop and integrate acceptance tests using SpecFlow with Selenium, authoring scenarios in Gherkin to validate complete user flows (registration, login, browsing, checkout).  
Rationale: ensure business scenarios are specified in Gherkin and validated end-to-end, reducing regressions in critical features.

## Acceptance Criteria
- User flows are defined as Gherkin feature files and implemented with SpecFlow and Selenium for scenarios: registration, login, product listing, cart operations, and checkout.  
- SpecFlow is configured to run Selenium-based tests against a containerized application instance or test server.  
- Test project includes step definitions in C# aligned with Gherkin scenarios.  
- Execution of SpecFlow tests and generation of Gherkin reports are integrated into the CI pipeline.  
- Failures in SpecFlow acceptance tests fail the build and block PR merges.

## Estimate
5 points

## Priority
High

## Assignee
—

## Dependencies
- 11. Increase Unit Test Coverage

## Attachments & References
- docs/infrastructure/testing-strategy.md  
- docs/roadmap/release-plan.md

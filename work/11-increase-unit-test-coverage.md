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
5 points

## Priority
High

## Assignee
—

## Dependencies
- 03. Finalize CQRS Handlers Implementation  
- 05. Refine Domain Validation & Invariants

## Attachments & References
- docs/infrastructure/testing-strategy.md

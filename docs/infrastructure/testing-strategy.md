# Testing Strategy

This document outlines the testing approach for FakeStoreNet across different levels.

## Unit Tests
- Framework: xUnit  
- Scope: Domain entities, value objects, domain services  
- Tools: FluentAssertions for expressive assertions  
- Goal: >80% coverage on domain and application layers

## Integration Tests
- Framework: xUnit + Microsoft.AspNetCore.Mvc.Testing TestServer  
- Scope: Interaction between API controllers, application services, and in-memory or test database  
- Tools: TestServer, EntityFrameworkCore InMemory provider  
- Goals: Validate end-to-end request handling, database integration

## Acceptance Tests (E2E)
- Framework: xUnit + RestSharp or HttpClient  
- Scope: Complete business flows against a running service instance (e.g., docker-compose)  
- Examples:
  - Product catalog CRUD  
  - User registration and authentication  
  - Cart lifecycle: create, add/remove items, checkout  
- Environments: Local, CI pipeline, staging

## Continuous Integration
- Automate test suite execution on pull requests (GitHub Actions or similar)  
- Fail build on test failures or coverage drop  
- Publish test reports and coverage metrics

## Regression & Smoke Tests
- Periodic smoke tests against staging or production endpoints (health check, sample CRUD)  
- Alert on unexpected failures

## Mocking & Test Data
- Use fixtures or builder patterns to generate valid domain objects  
- Mock external dependencies (e.g., email, third-party APIs) where applicable

This strategy ensures reliability, maintainability, and early detection of regressions across all layers of the FakeStoreNet solution.

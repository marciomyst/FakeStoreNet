# Product Backlog

This backlog captures outstanding items, features, and improvements for FakeStoreNet.

## Core Domain
- Stabilize entities and value objects
- Implement domain event dispatching (e.g., `ProductCreatedEvent`)
- Enhance validation rules and invariants

## Application Layer
- Finalize CQRS handlers for all commands and queries
- Implement caching for frequently accessed queries
- Add pagination and filtering to `GetAllProductsQuery`

## Infrastructure
- Add support for PostgreSQL provider
- Implement database migrations via EF Core or Flyway
- Integrate message broker (e.g., RabbitMQ) for event publishing

## API Enhancements
- Expand OpenAPI documentation for all endpoints
- Add health checks and readiness probes
- Secure endpoints with role-based authorization

## Testing & CI/CD
- Increase unit test coverage to 100% in Domain and Application
- Automate database migrations in CI pipeline
- Add acceptance tests for all user flows

## Observability & Monitoring
- Configure Grafana dashboards for metrics and logs
- Implement distributed tracing for end-to-end request flow
- Define alert thresholds and notification channels

## Future Roadmap
- Modularize Generic Domain as a NuGet package
- Extend API for order and payment processing
- Internationalization support for multi-language catalogs

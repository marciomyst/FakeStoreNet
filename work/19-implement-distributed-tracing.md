# 19. Implement Distributed Tracing with OpenTelemetry

## Description
Add distributed tracing instrumentation throughout the application using OpenTelemetry with DataDog exporter, collecting spans for critical operations and propagating context between services.  
Rationale: enable end-to-end performance analysis in DataDog, identify bottlenecks, and facilitate diagnostics in distributed environments.

## Acceptance Criteria
- OpenTelemetry SDK is configured in the API and domain components.  
- Spans are generated for HTTP requests, CQRS handlers, and interactions with the database and broker.  
- DataDog exporter is configured to send spans to DataDog APM.  
- Tracing context is correctly propagated between services and listeners.  
- Integration tests validate that spans are generated and exported.

## Estimate
3 points

## Priority
Medium

## Assignee
—

## Dependencies
- 12. Add Health Checks & Readiness Probes  
- 14. Configure DataDog for Metrics & Logs

## Attachments & References
- docs/infrastructure/observability.md  
- docs/infrastructure/deployment/docker-compose.yml

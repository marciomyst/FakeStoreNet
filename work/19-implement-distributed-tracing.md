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

## Technical Refinement

- OpenTelemetry SDK Configuration:
  - Add `OpenTelemetry.Extensions.Hosting`, `OpenTelemetry.Instrumentation.AspNetCore`, `OpenTelemetry.Instrumentation.Http`, `OpenTelemetry.Instrumentation.EntityFrameworkCore`, and `OpenTelemetry.Instrumentation.SqlClient` packages.
  - Configure tracing in `Program.cs`:
    ```csharp
    services.AddOpenTelemetryTracing(builder => builder
      .AddAspNetCoreInstrumentation()
      .AddHttpClientInstrumentation()
      .AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true)
      .SetSampler(new AlwaysOnSampler())
      .AddOtlpExporter(opt =>
      {
        opt.Endpoint = new Uri(configuration["Tracing:OtlpEndpoint"]);
        opt.Protocol = OtlpExportProtocol.HttpProtobuf;
      }));
    ```
  - Load OTLP endpoint and API key from configuration or environment variables (`Tracing:OtlpEndpoint`, `Tracing:ApiKey`).

- Context Propagation:
  - Use `Propagators.DefaultTextMapPropagator = new CompositeTextMapPropagator(new TextMapPropagator[] { new TraceContextPropagator(), new BaggagePropagator() });` to propagate context across HTTP and messaging boundaries.
  - Enrich `Activity` with custom tags (`service.name`, `environment`, `order.id`).

- Custom Spans & Instrumentation:
  - Manually create spans for key operations (e.g., order processing, event dispatch):
    ```csharp
    using var span = tracer.StartActiveSpan("ProcessOrder");
    span.SetAttribute("order.id", orderId);
    // domain logic
    ```

- Testing & Validation:
  - Use an OTLP test collector in Docker Compose to capture spans during integration tests.
  - Implement a `TestActivityExporter` to assert span creation and exported attributes in unit tests.
  - Verify end-to-end span ingestion in a local DataDog APM or OTLP-compatible collector.

This section details configuration and custom instrumentation for end-to-end distributed tracing with OpenTelemetry.

## Test Cases

```gherkin
Feature: Distributed Tracing

  Scenario: HTTP request spans are generated
    Given the API receives a GET request to /api/products
    When the request is processed
    Then an OpenTelemetry span for the HTTP request should be created and exported

  Scenario: CQRS handler span creation
    Given a CreateOrderCommandHandler executes
    When the handler logic runs
    Then a span named "CreateOrderCommandHandler" should be generated with attribute "order.id"

  Scenario: Database instrumentation
    Given a database query is executed via EF Core
    When OpenTelemetry EF instrumentation is enabled
    Then a span representing the SQL statement should be created and include the query text

  Scenario: Context propagation across messaging
    Given an OrderCreatedEvent is published to RabbitMQ
    When a consumer handles the event
    Then the consumer's span should be a child of the original publishing span
```

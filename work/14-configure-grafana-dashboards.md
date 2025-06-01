# 14. Configure DataDog for Metrics & Logs

## Description
Configure DataDog integration with Serilog to collect and visualize metrics and structured logs, covering key performance indicators and error tracking.  
Rationale: provide real-time visibility into application behavior using DataDog's monitoring and logging platform.

## Acceptance Criteria
- DataDog dashboards display:  
  - Request rate, latency (distribution), and error rate metrics.  
  - Resource usage metrics (CPU, memory) of the service.  
  - Structured logs from Serilog with filters for level, source, and correlation.  
- DataDog agent is configured and sending metrics and logs.  
- Dashboards and monitors are versioned in the repository as JSON.  
- Basic monitors and alerts with defined thresholds are configured in DataDog.

## Estimate
**Story Points:** 3 points

## Duration
**Junior Developer:** 27 hours
**Regular Developer:** 18 hours
**Senior Developer:** 12 hours
## Priority
Medium

## Assignee
—

## Dependencies
- 12. Add Health Checks & Readiness Probes  
- 09. Integrate Message Broker for Event Publishing

## Attachments & References
- docs/infrastructure/observability.md  
- docs/infrastructure/deployment/docker-compose.yml

## Technical Refinement

- DataDog Integration:
  - Configure Serilog sinks (`Serilog.Sinks.Datadog.Logs` and `Serilog.Sinks.Datadog.Metrics`) via DI:
    ```csharp
    .WriteTo.DatadogLogs(apiKey, source: "service", service: "FakeStore", host: env.MachineName)
    .WriteTo.DatadogMetrics(apiKey);
    ```
  - Load API key, environment, and service name from `appsettings.json` or environment variables.
  - Enrich logs with contextual properties: `CorrelationId`, `RequestId`, `UserId` using `Enrich.FromLogContext()`.

- Dashboard as Code:
  - Define DataDog dashboards in JSON/definition files and store under `infrastructure/monitoring/dashboards/`.
  - Use DataDog Terraform provider or CI scripts to push and version dashboards:
    ```bash
    datadog monitor create --path dashboards/api.json
    ```

- Monitors & Alerts:
  - Create monitors for CPU, memory, request latency (p95), and error rates using JSON definitions.
  - Define notification channels (Slack, email) and configure threshold triggers (e.g., latency > 500ms for 5m).

- Testing & Validation:
  - Write integration tests to verify metrics are emitted using DogStatsD client.
  - Simulate error and load scenarios to confirm dashboards and alerts respond correctly.

This section provides detailed technical guidance for integrating DataDog metrics, logs, dashboards, and alerts.

## Test Cases

```gherkin
Feature: DataDog Integration

  Scenario: Metrics sink sends request rate metric to DataDog
    Given DataDogMetrics sink is configured
    When application emits request rate metric
    Then DataDogMetrics sink should send metric with correct name and tags

  Scenario: Logs are sent to DataDog
    Given Serilog is configured with DatadogLogs sink
    When a structured log event is written
    Then the event should be sent to DataDog Logs with correct JSON payload

  Scenario: Dashboard JSON definitions applied correctly
    Given dashboard definition file exists under infrastructure/monitoring/dashboards
    When dashboard push script runs
    Then DataDog API should receive dashboard JSON and create/update dashboard without errors
```


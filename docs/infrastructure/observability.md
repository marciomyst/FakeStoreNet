# Observability Documentation

This document outlines how to monitor, log, and alert for the FakeStoreNet application.

## Metrics
- **Request Latency**: track response time for critical endpoints (`/products`, `/users`, `/carts`).
- **Error Rates**: count 4xx and 5xx responses, categorize by endpoint.
- **Database Performance**: monitor query duration and connection pool usage.

Use Prometheus to scrape metrics exposed by ASP.NET Core (e.g., via `prometheus-net`).

## Logging
- Structured logging using Serilog:
  - Enrich logs with `RequestId`, `UserId`, and `CorrelationId`.
  - Output to console (development) and to a centralized sink (e.g., Elasticsearch).
- Log levels:
  - **Error**: unhandled exceptions, failed operations.
  - **Warning**: business rule violations (e.g., adding zero quantity).
  - **Information**: high-level events (e.g., user registration, cart creation).

## Tracing
- Distributed tracing with OpenTelemetry:
  - Instrument HTTP pipeline, Entity Framework Core queries, and custom domain operations.
  - Export traces to Jaeger or Zipkin for request flow analysis.

## Alerting
- Configure alerts in Prometheus Alertmanager:
  - High error rate (>5% HTTP 5xx).
  - Elevated request latency (p95 > 500ms).
  - Database connection errors.
- Send alerts via email or Slack channel.

## Dashboards
- Grafana dashboard panels for:
  - API throughput and latency.
  - Error breakdown by service.
  - Database health and resource usage.

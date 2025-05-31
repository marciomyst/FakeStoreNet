# Release Plan

This document outlines planned releases, milestones, and delivery dates for FakeStoreNet.

## Milestone 1: v1.0.0 – Core Domain & API Basics
- **Features**:
  - Product catalog CRUD  
  - User registration and authentication  
  - Cart creation, item add/remove, checkout  
  - Domain model stabilization, value objects, and repositories  
  - OpenAPI documentation for core endpoints  
- **Acceptance Criteria**:
  - All API endpoints pass E2E tests  
  - ≥ 80% unit-test coverage in Domain & Application layers  
  - Basic CI pipeline with build and test steps

## Milestone 2: v1.1.0 – Validation & Error Handling
- **Features**:
  - Enhanced input validation (FluentValidation)  
  - Global exception handling and consistent error payloads  
  - Business-rule violation behaviors and error mapping  
- **Acceptance Criteria**:
  - Error responses conform to JSON API specification  
  - Integration tests validate error scenarios

## Milestone 3: v1.2.0 – Observability & Resilience
- **Features**:
  - Metrics (Prometheus) and structured logging (Serilog)  
  - Distributed tracing instrumentation (OpenTelemetry)  
  - Health checks and readiness probes  
- **Acceptance Criteria**:
  - Metrics scraped and visualized in Grafana  
  - Traces visible in Jaeger/Zipkin  
  - Health endpoint returns status UP

## Milestone 4: v2.0.0 – Extensions & Generic Domain
- **Features**:
  - Order processing module (new bounded context)  
  - Messaging integration (RabbitMQ) for domain events  
  - Extract Generic Domain as reusable NuGet package  
- **Acceptance Criteria**:
  - New “Orders” context with event-driven integration tests  
  - Messaging consumer and publisher verified in integration environment

## Next Steps
- Review and adjust based on stakeholder feedback  
- Prepare release notes and upgrade guides  
- Communicate timeline to product and engineering teams

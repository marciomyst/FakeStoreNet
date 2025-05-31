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
3 points

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

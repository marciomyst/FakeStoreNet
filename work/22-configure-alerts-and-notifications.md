# 22. Configure Alerts & Notifications

## Description
Define and implement alerting rules in Prometheus Alertmanager for critical thresholds (errors, latency, availability) and configure notification channels (Slack, email).  
Rationale: ensure immediate incident response, reduce resolution time, and maintain availability SLAs.

## Acceptance Criteria
- Alert rules created in Alertmanager for:  
  - HTTP 5xx error rate > 5% over a 10-minute window.  
  - p95 latency > 500ms for critical endpoints.  
  - Database connection errors detected.  
- Notification channels configured:  
  - Slack (webhook or official integration)  
  - Email (SMTP or external service)  
- Alerts tested in the staging environment with at least one channel receiving a notification.  
- Documentation updated in `docs/infrastructure/observability.md` and the deployment runbook.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 12. Add Health Checks & Readiness Probes  
- 14. Configure Grafana Dashboards for Metrics & Logs

## Attachments & References
- docs/infrastructure/observability.md  
- docs/infrastructure/deployment/runbook.md

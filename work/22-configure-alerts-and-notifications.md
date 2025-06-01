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

## Technical Refinement

- Prometheus Alert Rules:
  - Define alert rules in `infrastructure/monitoring/alerts/alerts.yaml`:
    ```yaml
    groups:
      - name: fake-store-alerts
        rules:
          - alert: HighErrorRate
            expr: increase(http_requests_total{status=~"5.."}[10m]) / increase(http_requests_total[10m]) > 0.05
            for: 10m
            labels:
              severity: critical
            annotations:
              summary: "HTTP 5xx error rate > 5% in 10m"
          - alert: HighLatency
            expr: histogram_quantile(0.95, sum(rate(http_request_duration_seconds_bucket[5m])) by (le, route)) > 0.5
            for: 5m
            labels:
              severity: warning
            annotations:
              summary: "p95 latency > 500ms for critical endpoints"
          - alert: DatabaseConnectionErrors
            expr: increase(db_connection_errors_total[5m]) > 0
            for: 5m
            labels:
              severity: critical
            annotations:
              summary: "Database connection errors detected"
    ```

- Alertmanager Configuration:
  - Store `alertmanager.yml` under `infrastructure/monitoring/alerts/alertmanager.yml`:
    ```yaml
    global:
      resolve_timeout: 5m
    route:
      receiver: "slack"
    receivers:
      - name: "slack"
        slack_configs:
          - channel: "#alerts"
            api_url: ${SLACK_WEBHOOK_URL}
      - name: "email"
        email_configs:
          - to: ${ALERT_EMAIL_RECIPIENT}
            from: ${SMTP_FROM}
            smarthost: ${SMTP_HOST}:${SMTP_PORT}
    ```

- Notification Channels:
  - Configure Slack webhook URL and SMTP credentials via environment variables or CI secrets (`SLACK_WEBHOOK_URL`, `SMTP_HOST`, etc.).
  - Use secure vault or CI secret management for sensitive configuration.

- Testing & Validation:
  - Write integration tests or use Prometheus test framework to simulate alert conditions and verify notifications are sent.
  - Validate that alerts trigger in staging environment and Slack/email channels receive messages.

This section outlines the detailed technical steps for defining, configuring, and validating alerts and notifications in the monitoring infrastructure.

## Test Cases

```gherkin
Feature: Alerts & Notifications

  Scenario: Trigger high error rate alert
    Given HTTP 5xx error rate > 5% over 10m
    When Prometheus Alertmanager evaluates rules
    Then the "HighErrorRate" alert should fire

  Scenario: Send Slack notification for alert
    Given "HighErrorRate" alert is firing
    When Alertmanager routes alert
    Then a Slack notification should be sent to "#alerts"

  Scenario: Send email notification for database errors
    Given "DatabaseConnectionErrors" alert is firing
    When Alertmanager routes alert
    Then an email should be sent to configured email recipient

  Scenario: No alert when metrics below thresholds
    Given HTTP 5xx error rate < 1% over 10m
    When Prometheus Alertmanager evaluates rules
    Then no alert should fire
```

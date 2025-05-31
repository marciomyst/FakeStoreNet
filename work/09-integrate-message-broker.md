# 09. Integrate Message Broker for Event Publishing

## Description
Configure Azure Service Bus with Rebus for domain event publishing and consumption, enabling asynchronous integration between modules.  
Rationale: support decoupled, scalable, and resilient communication via Azure Service Bus and Rebus, allowing future extensions without direct coupling.

## Acceptance Criteria
- An Azure Service Bus namespace is configured and accessible.  
- The EventBus publisher uses Rebus with Azure Service Bus transport to send messages.  
- A sample Rebus consumer receives and processes messages from Azure Service Bus correctly.  
- Connection settings and credentials are parameterizable via `appsettings.json` or environment variables.  
- Integration tests verify the sending and consumption of at least one event type.

## Estimate
5 points

## Priority
High

## Assignee
—

## Dependencies
- 02. Implement Domain Event Dispatching

## Attachments & References
- docs/infrastructure/deployment/runbook.md  
- docs/tactical/modeling/domain-events.md

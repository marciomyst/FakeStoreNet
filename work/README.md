# Work Items Index

This directory contains individual work item files, each describing a valuable increment for the FakeStoreNet project. All work items are written in English.

## List of Work Items

1. [01-stabilize-entities-and-value-objects.md](01-stabilize-entities-and-value-objects.md) – Stabilize Entities & Value Objects  
2. [02-implement-domain-event-dispatching.md](02-implement-domain-event-dispatching.md) – Implement Domain Event Dispatching  
3. [03-finalize-cqrs-handlers.md](03-finalize-cqrs-handlers.md) – Finalize CQRS Handlers  
4. [04-implement-caching-for-frequent-queries.md](04-implement-caching-for-frequent-queries.md) – Implement Caching for Frequent Queries  
5. [05-refine-domain-validation-and-invariants.md](05-refine-domain-validation-and-invariants.md) – Refine Domain Validation & Invariants  
6. [06-add-pagination-and-filtering.md](06-add-pagination-and-filtering.md) – Add Pagination & Filtering  
7. [07-add-postgresql-provider-support.md](07-add-postgresql-provider-support.md) – Add PostgreSQL Provider Support  
8. [08-implement-database-migrations.md](08-implement-database-migrations.md) – Implement Database Migrations  
9. [09-integrate-message-broker.md](09-integrate-message-broker.md) – Integrate Message Broker  
10. [10-expand-openapi-documentation.md](10-expand-openapi-documentation.md) – Expand OpenAPI Documentation  
11. [11-increase-unit-test-coverage.md](11-increase-unit-test-coverage.md) – Increase Unit Test Coverage  
12. [12-add-health-checks-and-readiness-probes.md](12-add-health-checks-and-readiness-probes.md) – Add Health Checks & Readiness Probes  
13. [13-automate-migrations-ci-cd.md](13-automate-migrations-ci-cd.md) – Automate Database Migrations in CI/CD  
14. [14-configure-grafana-dashboards.md](14-configure-grafana-dashboards.md) – Configure Grafana Dashboards  
15. [15-implement-role-based-authorization.md](15-implement-role-based-authorization.md) – Implement Role-Based Authorization  
16. [16-add-acceptance-tests-for-user-flows.md](16-add-acceptance-tests-for-user-flows.md) – Add Acceptance Tests for User Flows  
17. [17-implement-order-processing-module.md](17-implement-order-processing-module.md) – Implement Order Processing Module  
18. [18-extract-generic-domain-as-nuget-package.md](18-extract-generic-domain-as-nuget-package.md) – Extract Generic Domain as NuGet Package  
19. [19-implement-distributed-tracing.md](19-implement-distributed-tracing.md) – Implement Distributed Tracing  
20. [20-implement-internationalization-and-localization.md](20-implement-internationalization-and-localization.md) – Implement Internationalization & Localization  

## Work Item Flow & Dependencies (Git Diagram)

```mermaid
graph TD
  1[Stabilize Entities & Value Objects]
  2[Implement Domain Event Dispatching]
  3[Finalize CQRS Handlers]
  4[Implement Caching for Frequent Queries]
  5[Refine Domain Validation & Invariants]
  6[Add Pagination & Filtering]
  7[Add PostgreSQL Provider Support]
  8[Implement Database Migrations]
  9[Integrate Message Broker]
  10[Expand OpenAPI Documentation]
  11[Increase Unit Test Coverage]
  12[Add Health Checks & Readiness Probes]
  13[Automate Database Migrations in CI/CD]
  14[Configure Grafana Dashboards]
  15[Implement Role-Based Authorization]
  16[Add Acceptance Tests for User Flows]
  17[Implement Order Processing Module]
  18[Extract Generic Domain as NuGet Package]
  19[Implement Distributed Tracing]
  20[Implement Internationalization & Localization]

  1 --> 2
  1 --> 3
  1 --> 5
  1 --> 7
  1 --> 17

  2 --> 3
  2 --> 9
  2 --> 17

  3 --> 4
  3 --> 6
  3 --> 10
  3 --> 11
  3 --> 15
  3 --> 17
  3 --> 18

  4 --> 12
  8 --> 12
  9 --> 14
  12 --> 14
  12 --> 19

  5 --> 11
  5 --> 20

  6 --> 

  7 --> 8
  7 --> 12

  8 --> 13

  9 --> 14

  10 --> 15

  11 --> 16

  14 --> 19

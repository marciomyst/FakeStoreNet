# 05. Refine Domain Validation & Invariants

## Description
Enhance validation rules across all entities and value objects, ensuring business invariants are consistently enforced.  
Rationale: prevent invalid states in the domain, improving reliability and reducing production errors.

## Acceptance Criteria
- FluentValidation or native validations are implemented for all relevant commands.  
- Invariants (e.g., price ≥ 0, stock ≥ 0, valid email) are verified before persistence.  
- Standardized and informative error feedback is provided for rule violations.  
- Unit tests cover both positive and negative validation scenarios for all entities.

## Estimate
3 points

## Priority
High

## Assignee
—

## Dependencies
- 01. Stabilize Entities & Value Objects

## Attachments & References
- docs/tactical/modeling/business-rules.md  
- docs/infrastructure/api/openapi-customers.yaml

# 20. Implement Internationalization & Localization

## Description
Add support for multiple languages and regional formats in the application, enabling text translations and locale-aware data formatting.  
Rationale: expand product reach to users in different regions, improving global experience and accessibility.

## Acceptance Criteria
- Resource files (`.resx` or equivalent) are configured for supported languages (e.g., pt-BR, en-US).  
- Localization middleware is enabled in the ASP.NET Core pipeline.  
- Static frontend strings and backend error messages load correct translations based on `Accept-Language` or user settings.  
- Date, number, and currency formats adapt to the locale.  
- Integration tests validate language switching and formatting.

## Estimate
5 points

## Priority
Medium

## Assignee
—

## Dependencies
- 05. Refine Domain Validation & Invariants

## Attachments & References
- docs/roadmap/release-plan.md  
- docs/glossary/terms.md

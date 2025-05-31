# Subdomain Classification for FakeStoreNet

Based on strategic design, the FakeStoreNet solution is organized into three subdomain types:

## Core Domain (E-commerce)
- **Product Catalog**: management of product listings, details, and pricing.
- **User Management**: registration, profile management, and credential handling.
- **Cart Management**: creation of shopping carts, item addition/removal, and cart lifecycle.
- **Authentication**: user login workflow and JWT token generation.

## Supporting Domain
- **Authentication & Security**: JWT token generation/validation, password policies.
- **Persistence Configuration**: repository abstractions and ORM mapping configurations.

## Generic Domain
- **Messaging / Logging**: cross-cutting infrastructure concerns that may be factored out.
- **Validation / Serialization**: shared behaviors, for example the ValidationBehavior pipeline.

These classifications guide team ownership and help isolate changes within each subdomain.

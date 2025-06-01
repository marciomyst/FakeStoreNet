# 21. Implement Payment Processing

## Description
Develop integration with a payment provider (e.g., Stripe, PayPal) to process charges during the checkout flow, ensuring secure payment capture and error handling.  
Rationale: enable revenue generation by completing sales and providing a reliable payment experience for users.

## Acceptance Criteria
- Payment module integrates with provider API (sandbox) for payment authorization and capture.  
- Checkout flow updates order status only after payment confirmation.  
- Failure scenarios (payment declined, timeout) are handled with appropriate user messages.  
- Webhooks or callbacks are configured to update payment status asynchronously.  
- Integration tests validate the full payment flow (authorization → capture).

## Estimate
**Story Points:** 5 points

## Duration
**Junior Developer:** 45 hours
**Regular Developer:** 30 hours
**Senior Developer:** 20 hours
## Priority
High

## Assignee
—

## Dependencies
- 17. Implement Order Processing Module  
- 09. Integrate Message Broker for Event Publishing

## Attachments & References
- docs/roadmap/release-plan.md  
- docs/infrastructure/api/openapi-orders.yaml  
- docs/infrastructure/deployment/docker-compose.yml

## Technical Refinement

- Payment SDK Configuration:
  - Configure payment provider options in `appsettings.json` and DI:
    ```csharp
    services.Configure<StripeOptions>(Configuration.GetSection("Stripe"));
    services.AddTransient<IPaymentService, StripePaymentService>();
    ```
  - Load API keys and environment (sandbox/production) via secure configuration.

- Payment Service Implementation:
  - Define `IPaymentService` with methods `AuthorizePaymentAsync`, `CapturePaymentAsync`, `RefundPaymentAsync`, and `HandleWebhookAsync`.
  - Implement `StripePaymentService` using Stripe .NET SDK, mapping Stripe exceptions to domain `PaymentException`.
  - Ensure idempotency by storing and checking `PaymentIntentId`.

- Checkout Flow Integration:
  - In `CreateOrderCommandHandler` or `CheckoutHandler`, call `AuthorizePaymentAsync` before committing the order.
  - Capture payment upon order confirmation using `CapturePaymentAsync`.
  - Roll back domain transaction if authorization fails and publish `PaymentFailedEvent`.

- Webhook Endpoint:
  - Expose `/api/payments/webhook` to receive events (e.g., `payment_intent.succeeded`, `payment_intent.payment_failed`).
  - Validate webhook signature header with `StripeEventUtility.ConstructEvent`.
  - Update order status in response to webhook events and publish corresponding domain events.

- Error Handling & Resilience:
  - Apply retry policies (e.g., Polly) for transient HTTP errors and rate limits.
  - Publish domain events (`PaymentSucceededEvent`, `PaymentFailedEvent`) for asynchronous processing.

- Security & Compliance:
  - Do not store raw payment data; use tokenization.
  - Ensure PCI DSS compliance by only handling tokens and using secure channels.

- Testing Strategy:
  - Unit tests mocking `IPaymentService` to simulate success, decline, and error scenarios.
  - Integration tests against Stripe sandbox or local emulator (e.g., Stripe CLI forward).
  - Validate webhook processing and order state transitions in tests.

This section defines the technical details needed for robust, secure, and maintainable payment processing integration.

## Test Cases

```gherkin
Feature: Payment Processing Integration

  Scenario: Authorize payment successfully
    Given valid payment details for amount 100.00 and currency "USD"
    When AuthorizePaymentAsync is called
    Then a payment intent should be created with status "requires_capture"

  Scenario: Capture payment after authorization
    Given a valid payment intent ID "pi_123"
    When CapturePaymentAsync is called with "pi_123"
    Then the payment should be captured successfully with status "succeeded"

  Scenario: Handle payment decline
    Given the payment provider returns a decline error for authorization
    When AuthorizePaymentAsync is called
    Then a PaymentException should be thrown with message containing "declined"

  Scenario: Process webhook for payment succeeded
    Given a webhook event "payment_intent.succeeded" payload is received
    When HandleWebhookAsync is called with the event payload
    Then the corresponding Order status should be updated to "Paid"
```


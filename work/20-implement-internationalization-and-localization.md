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
**Story Points:** 5 points

## Duration
**Junior Developer:** 45 hours
**Regular Developer:** 30 hours
**Senior Developer:** 20 hours
## Priority
Medium

## Assignee
—

## Dependencies
- 05. Refine Domain Validation & Invariants

## Attachments & References
- docs/roadmap/release-plan.md  
- docs/glossary/terms.md

## Technical Refinement

- Localization Resources:
  - Organize resource files (`.resx`) by culture under `Resources/` (e.g., `SharedResource.en-US.resx`, `SharedResource.pt-BR.resx`).
  - Use `IStringLocalizer<SharedResource>` in controllers and services to retrieve localized strings.
  - Implement fallback logic to default culture for missing translations.

- Middleware Configuration:
  - Register localization services:  
    ```csharp
    services.AddLocalization(options => options.ResourcesPath = "Resources");
    ```
  - Configure request localization:  
    ```csharp
    var supportedCultures = new[] { "en-US", "pt-BR" };
    app.UseRequestLocalization(new RequestLocalizationOptions
    {
      DefaultRequestCulture = new RequestCulture("en-US"),
      SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList(),
      SupportedUICultures = supportedCultures.Select(c => new CultureInfo(c)).ToList()
    });
    ```

- Data Formatting:
  - Rely on `CultureInfo.CurrentCulture` for formatting dates, numbers, and currency (e.g., `value.ToString("C")`).
  - Configure JSON localization by setting `JsonOptions.SerializerOptions.PropertyNamingPolicy` and `FormatterOptions` to respect current culture.

- API & UI Integration:
  - Respect `Accept-Language` header in API and expose language selector in UI to override locale.
  - Document supported languages and header usage in Swagger UI.

- Testing Strategy:
  - Unit test localization retrieval with in-memory `ResourceManagerStringLocalizer`.
- Integration tests sending requests with different `Accept-Language` headers to verify localized responses.

## Test Cases

```gherkin
Feature: Internationalization and Localization

  Scenario: Default language fallback to en-US
    Given no Accept-Language header is provided
    When a request is made to "/api/products"
    Then the response messages should be in "en-US"

  Scenario: Load pt-BR translations
    Given Accept-Language header "pt-BR"
    When a request is made to "/api/orders/123"
    Then the error messages and date formats should be in Brazilian Portuguese

  Scenario: Format currency according to locale
    Given Accept-Language header "pt-BR"
    When the API returns a monetary value
    Then the value should be formatted with comma as decimal separator and currency symbol

  Scenario: Fallback to default for unsupported culture
    Given Accept-Language header "fr-FR"
    When a request is made to "/api/products"
    Then the response should use default culture "en-US"
```


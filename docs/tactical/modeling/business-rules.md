# Business Rules Specification

This document defines the business rules and invariants enforced within the FakeStoreNet domain.

## 1. Product

1. **Non-negative Price**  
   - `Price` must be ≥ 0.  
   - Violation triggers `BusinessRuleViolationException`.

2. **Required Title**  
   - `Title` cannot be null or empty.

3. **Optional Description**  
   - `Description` may be empty but must not exceed 1000 characters.

4. **Required Category**  
   - `Category` cannot be null or empty.

5. **Valid Image URL**  
   - `Image` must be a well-formed URI.

6. **Rating Constraints**  
   - `Rating.Rate` between 0.0 and 5.0.  
   - `Rating.Count` ≥ 0.

## 2. User

1. **Required Username**  
   - `Username` cannot be null or empty.

2. **Valid Email**  
   - `Email` must match a standard email pattern.

3. **Strong Password**  
   - `Password` must be at least 8 characters and include uppercase, lowercase, and digits.

4. **Complete Name**  
   - `Name.FirstName` and `Name.LastName` cannot be null or empty.

5. **Consistent Address**  
   - `Street`, `City`, and `ZipCode` cannot be null or empty.  
   - `Geolocation.Latitude` and `Longitude` must be valid coordinates.

## 3. Cart & CartItem

1. **Minimum Quantity**  
   - `CartItem.Quantity` must be > 0.

2. **Unique Items**  
   - Adding the same `ProductId` accumulates quantity rather than duplicating items.

3. **Cart Initialization**  
   - Creating a `Cart` sets the current date and a valid `UserId`.

4. **Removing Non-Existent Item**  
   - Removing an item not in the cart throws `EntityNotFoundException`.

## 4. Authentication

1. **Credential Validation**  
   - `Authenticate(username, password)` validates an existing user and correct password.  
   - Failure throws `UnauthorizedException`.

2. **JWT Token**  
   - Token includes `UserId`, `Username`, and expires per `JwtSettings.ExpiryMinutes`.

## 5. Repositories & Persistence

1. **Existence Check**  
   - `GetById(id)` throws `EntityNotFoundException` if not found.

2. **Aggregate Persistence**  
   - `Add` and `Update` must enforce invariants before saving.

3. **Atomic Transactions**  
   - Composite operations occur within a single `FakeStoreDbContext` transaction.

## 6. Domain Exceptions

- `EntityNotFoundException`: thrown when an entity is missing.  
- `BusinessRuleViolationException`: thrown on invariant violations.  
- `UnauthorizedException`: thrown on authentication failure.

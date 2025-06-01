# Value Objects

Value Objects encapsulate immutable concepts with equality by value and internal invariants.

## Name

Represents a user’s full name.

- `FirstName` (string, non-empty)  
- `LastName` (string, non-empty)  
- Implements `Equals` and `GetHashCode` for value equality.

## Address

Represents a postal address.

- `Street` (string, non-empty)  
- `Number` (string, non-empty)  
- `City` (string, non-empty)  
- `ZipCode` (string, non-empty, valid postal format)  
- `Geolocation` (Value Object)

## Geolocation

Encapsulates geographic coordinates.

- `Latitude` (decimal, -90 ≤ latitude ≤ 90)  
- `Longitude` (decimal, -180 ≤ longitude ≤ 180)

## Rating

Captures product rating.

- `Rate` (double, 0.0 – 5.0)  
- `Count` (int, ≥ 0)

All Value Objects validate invariants in their constructors and remain immutable.

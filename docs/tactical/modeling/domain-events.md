# Domain Event Catalog

This catalog describes the domain events emitted by the aggregates in FakeStoreNet.

## Product Events

### ProductCreatedEvent
- **Description**: Fired when a new product is added.
- **Payload**:
  - `productId` (integer)
  - `title` (string)
  - `price` (number)
  - `category` (string)

### ProductPriceChangedEvent
- **Description**: Fired when a product’s price is updated.
- **Payload**:
  - `productId` (integer)
  - `oldPrice` (number)
  - `newPrice` (number)

## Cart Events

### CartCreatedEvent
- **Description**: Fired when a cart is created for a user.
- **Payload**:
  - `cartId` (integer)
  - `userId` (integer)
  - `createdDate` (string, date-time)

### CartItemAddedEvent
- **Description**: Fired when an item is added to a cart.
- **Payload**:
  - `cartId` (integer)
  - `productId` (integer)
  - `quantity` (integer)

### CartItemRemovedEvent
- **Description**: Fired when an item is removed from a cart.
- **Payload**:
  - `cartId` (integer)
  - `productId` (integer)

### CartCheckedOutEvent
- **Description**: Fired when a cart is checked out.
- **Payload**:
  - `cartId` (integer)
  - `userId` (integer)
  - `checkedOutDate` (string, date-time)

## User Events

### UserRegisteredEvent
- **Description**: Fired after a user registers.
- **Payload**:
  - `userId` (integer)
  - `username` (string)
  - `email` (string)

### UserAuthenticatedEvent
- **Description**: Fired after successful login.
- **Payload**:
  - `userId` (integer)
  - `username` (string)
  - `authenticatedAt` (string, date-time)

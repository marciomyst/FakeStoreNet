# Ubiquitous Language Glossary

| Term                                                 | Definition                                                                                                          |
| ---------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------- |
| Address                                              | Value object representing a user’s address (street, number, city, postal code, geolocation).                        |
| AuthService                                          | Domain service responsible for authenticating users and validating credentials.                                     |
| BusinessRuleViolationException                       | Exception thrown when a business rule is violated (e.g., insufficient stock).                                       |
| Cart                                                 | Aggregate root representing a user’s shopping cart containing multiple CartItem entries.                            |
| CartItem                                             | Child entity of Cart linking a product to a desired quantity.                                                       |
| EntityNotFoundException                              | Exception thrown when an entity cannot be found in a repository.                                                    |
| Geolocation                                          | Value object containing latitude and longitude coordinates for an Address.                                          |
| ICartRepository, IProductRepository, IUserRepository | Repository interfaces abstracting persistence operations for Cart, Product, and User.                               |
| IAuthService                                         | Interface for the authentication service (login and JWT token generation).                                          |
| Name                                                 | Value object representing a user’s full name (first and last names).                                                |
| NotFoundException                                    | Application-layer exception indicating a requested resource was not found.                                          |
| Product                                              | Aggregate root representing an item available for purchase (title, price, description, category, image, rating).    |
| Rating                                               | Value object encapsulating a product’s rating (rate and count of reviews).                                          |
| Repositories                                         | Pattern for CRUD operations on aggregates: IProductRepository, ICartRepository, IUserRepository.                    |
| Services                                             | Layer of application and domain logic: IProductService, IUserService, IAuthService, AuthService, JwtTokenGenerator. |
| User                                                 | Aggregate root representing a customer or system user (username, email, password, name, address, phone).            |

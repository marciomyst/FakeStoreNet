# Repository Interfaces

Define contracts for persistence operations on aggregates.

## IProductRepository

```csharp
public interface IProductRepository
{
    Product GetById(int id);
    IEnumerable<Product> GetAll();
    void Add(Product product);
    void Update(Product product);
}
```

- `GetById(int id)`: retrieve a product or throw `EntityNotFoundException`.  
- `GetAll()`: list all products.  
- `Add(Product)`: persist a new product.  
- `Update(Product)`: save changes to an existing product.

## ICartRepository

```csharp
public interface ICartRepository
{
    Cart GetById(int id);
    void Add(Cart cart);
    void Update(Cart cart);
}
```

- `GetById(int id)`: retrieve a cart or throw `EntityNotFoundException`.  
- `Add(Cart)`: persist a new cart.  
- `Update(Cart)`: save changes to an existing cart.

## IUserRepository

```csharp
public interface IUserRepository
{
    User GetById(int id);
    IEnumerable<User> GetAll();
    void Add(User user);
}
```

- `GetById(int id)`: retrieve a user or throw `EntityNotFoundException`.  
- `GetAll()`: list all users.  
- `Add(User)`: persist a new user.

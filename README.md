# ShoppingTaskApi



## Shopping Cart Functionality

This project includes a real-time shopping cart feature implemented using SignalR and in-memory caching. The key components involved in this functionality are the `CartHub` class and the `ShoppingCart` class.

### CartHub Class

The `CartHub` class is a SignalR hub responsible for managing real-time communication related to shopping cart operations. It allows clients to interact with their shopping carts through various methods.

#### Key Features:

- **Authorization:** The `[Authorize]` attribute ensures that only authenticated users can access the methods in this hub.
  
- **Dependency Injection:** The `CartHub` constructor takes a `ShoppingCart` instance as a parameter, which is injected via dependency injection. This instance is used to manage the shopping cart items for the user.

- **Methods:**
  - **GetCartItems:** Retrieves the current items in the user's cart and sends them to the client.
  - **AddItem:** Adds a new item to the user's cart and notifies the client that the item has been added.
  - **UpdateItem:** Updates the quantity of an existing item in the user's cart and notifies the client of the update.
  - **RemoveItem:** Removes an item from the user's cart and notifies the client that the item has been removed.

Each method checks the user's identity to ensure that the operations are performed for the correct user.

### ShoppingCart Class

The `ShoppingCart` class is responsible for managing the shopping cart items in memory using the `IMemoryCache` interface provided by ASP.NET Core.

#### Key Features:

- **Caching:** The `ShoppingCart` class uses in-memory caching to store cart items for each user. The user's ID is used as the key to store and retrieve the cart itemsvand save it for (1 Day) then will be removed.

- **Methods:**
  - **GetCartItems:** Retrieves the cart items for a given user ID from the cache. If the items are not found in the cache, it returns an empty list.
  - **AddItem:** Adds a new item to the user's cart. It retrieves the current cart items, adds the new item, and updates the cache with the modified list. A sliding expiration of one hour is set for the cache entry.
  - **UpdateItem:** Updates the quantity of an existing item in the user's cart. It retrieves the current items, updates the quantity, and refreshes the cache.
  - **RemoveItem:** Removes an item from the user's cart. If the cart becomes empty after the removal, the cache entry for that user is removed. Otherwise, the cache is updated with the modified list.

### Integration of Caching with SignalR

The integration of caching with SignalR provides several benefits:

1. **Performance Optimization:** Caching cart items in memory allows for quick retrieval and manipulation of cart data, leading to faster response times for users.

2. **Real-Time Updates:** When a user adds, updates, or removes items from their cart, the corresponding SignalR methods are called. These methods interact with the `ShoppingCart` class to modify the cached data and notify the client of the changes in real-time.

3. **Reduced Database Load:** By using caching, the application minimizes the number of database operations required to manage cart items, which is particularly important in high-traffic scenarios.

4. **User -Specific Data:** The caching mechanism is user-specific, meaning that each user's cart items are stored separately based on their unique user ID, allowing for personalized shopping experiences.

### Conclusion

The combination of SignalR and in-memory caching in this project allows for efficient, real-time management of shopping cart operations. The `CartHub` class facilitates real-time communication with clients, while the `ShoppingCart` class ensures that cart data is stored and retrieved quickly, enhancing the overall user experience. This architecture effectively balances performance and usability, making it suitable for modern web applications.

### MediatR

[MediatR](https://github.com/jbogard/MediatR) is a simple, unambitious mediator implementation in .NET. It helps to decouple the application components by providing a way to send commands and queries without needing to know the details of the handlers. This promotes a clean architecture and adheres to the Single Responsibility Principle.

**Usage:**
- Commands and queries are sent through the MediatR interface, which then routes them to the appropriate handlers.
- This pattern helps in organizing code and makes it easier to maintain and test.

### Domain-Driven Design (DDD)

This project follows the principles of [Domain-Driven Design (DDD)](https://martinfowler.com/tags/domain%20driven%20design.html), which emphasizes collaboration between technical and domain experts to create a shared model of the domain. DDD helps in managing complex business logic and promotes a clear separation of concerns.

**Key Concepts:**
- **Entities:** Objects that have a distinct identity and lifecycle.
- **Value Objects:** Immutable objects that represent descriptive aspects of the domain.
- **Aggregates:** A cluster of domain objects that can be treated as a single unit.
- **Repositories:** Abstractions for accessing and managing aggregates.
- **Domain Events:** Events that signify a change in the state of the domain.

By leveraging these tools and architectural patterns, this project aims to create a robust, maintainable, and scalable application that meets the needs of its users.

## Getting Started

For more information on how to set up and run the project, please refer to the [Installation](#installation) section.
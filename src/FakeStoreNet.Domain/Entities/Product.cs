using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Domain.Common.Events;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents a product in the catalog.
    /// </summary>
    public class Product : Entity
    {
        /// <summary>
        /// Gets the title of the product.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the price of the product.
        /// </summary>
        public Money Price { get; private set; }

        /// <summary>
        /// Gets the product description.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the product category.
        /// </summary>
        public string Category { get; private set; }

        /// <summary>
        /// Gets the image URL for the product.
        /// </summary>
        public string Image { get; private set; }

        /// <summary>
        /// Gets the rating value object.
        /// </summary>
        public Rating Rating { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> entity.
        /// </summary>
        /// <param name="title">Product title. Cannot be null or empty.</param>
        /// <param name="price">Product price. Must be non-negative.</param>
        /// <param name="description">Product description.</param>
        /// <param name="category">Product category. Cannot be null or empty.</param>
        /// <param name="image">Image URL.</param>
        /// <param name="rating">Rating value object.</param>
        /// <exception cref="DomainValidationException">Thrown when any invariant is violated.</exception>
        public Product(string title, Money price, string description, string category, string image, Rating rating)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainValidationException("Title is required");

            Title = title;
            Price = price ?? throw new DomainValidationException("Price is required");
            Description = description;

            if (string.IsNullOrWhiteSpace(category))
                throw new DomainValidationException("Category is required");

            Category = category;
            Image = image;
            Rating = rating ?? throw new DomainValidationException("Rating is required");
            AddDomainEvent(new ProductCreatedEvent(Id, Id, Title, Price.Amount, Category));
        }

        /// <summary>
        /// Adjusts the price of the product.
        /// </summary>
        /// <param name="newPrice">The new price. Must be non-negative.</param>
        public void AdjustPrice(Money newPrice)
        {
            if (newPrice == null)
                throw new DomainValidationException("New price is required");

            Price = newPrice;
        }

        /// <summary>
        /// Updates product details and raises a <see cref="ProductUpdatedEvent"/>.
        /// </summary>
        /// <param name="title">New title. Cannot be null or empty.</param>
        /// <param name="price">New price. Must be non-negative.</param>
        /// <param name="description">New description.</param>
        /// <param name="category">New category. Cannot be null or empty.</param>
        /// <param name="image">New image URL.</param>
        /// <param name="rating">New rating. Cannot be null.</param>
        public void UpdateDetails(string title, Money price, string description, string category, string image, Rating rating)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainValidationException("Title is required");
            if (price == null)
                throw new DomainValidationException("Price is required");
            if (string.IsNullOrWhiteSpace(category))
                throw new DomainValidationException("Category is required");
            if (rating == null)
                throw new DomainValidationException("Rating is required");

            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
            Rating = rating;

            AddDomainEvent(new ProductUpdatedEvent(Id, Id, Title, Price.Amount, Category));
        }
    }
}

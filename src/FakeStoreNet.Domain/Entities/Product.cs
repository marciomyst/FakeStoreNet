using System;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Primary entity representing a product.
    /// </summary>
    public sealed class Product : IEquatable<Product>
    {
        public int Id { get; }
        public string Title { get; private set; }
        public Money Price { get; private set; }
        public string Description { get; private set; }
        public string Category { get; private set; }
        public string Image { get; private set; }
        public Rating Rating { get; private set; }

        public Product(
            int id,
            string title,
            Money price,
            string description,
            string category,
            string image,
            Rating rating)
        {
            if (id <= 0)
                throw new DomainValidationException("Id must be positive");
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainValidationException("Title is required");
            if (price is null)
                throw new DomainValidationException("Price is required");
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainValidationException("Description is required");
            if (string.IsNullOrWhiteSpace(category))
                throw new DomainValidationException("Category is required");
            if (string.IsNullOrWhiteSpace(image))
                throw new DomainValidationException("Image is required");
            if (rating is null)
                throw new DomainValidationException("Rating is required");

            Id = id;
            Title = title;
            Price = price;
            Description = description;
            Category = category;
            Image = image;
            Rating = rating;
        }

        public void AdjustPrice(Money newPrice)
        {
            if (newPrice is null)
                throw new DomainValidationException("New price is required");
            Price = newPrice;
        }

        public bool Equals(Product? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => Equals(obj as Product);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Product? left, Product? right) => Equals(left, right);

        public static bool operator !=(Product? left, Product? right) => !Equals(left, right);
    }
}

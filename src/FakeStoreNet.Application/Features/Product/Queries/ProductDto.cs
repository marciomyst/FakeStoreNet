using System;
namespace FakeStoreNet.Application.Features.Product.Queries
{
    /// <summary>
    /// Data Transfer Object for Product.
    /// </summary>
    public class ProductDto : IEquatable<ProductDto>
    {
        /// <summary>
        /// Identifier of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the product.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Category of the product.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Image URL of the product.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Average rating of the product.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Number of ratings.
        /// </summary>
        public int Count { get; set; }

        public bool Equals(ProductDto? other)
        {
            if (other == null) return false;
            return Id == other.Id
                && Title == other.Title
                && Price == other.Price
                && Description == other.Description
                && Category == other.Category
                && Image == other.Image
                && Rate == other.Rate
                && Count == other.Count;
        }

        public override bool Equals(object? obj) => Equals(obj as ProductDto);

        public override int GetHashCode() => HashCode.Combine(Id, Title, Price, Description, Category, Image, Rate, Count);
    }
}

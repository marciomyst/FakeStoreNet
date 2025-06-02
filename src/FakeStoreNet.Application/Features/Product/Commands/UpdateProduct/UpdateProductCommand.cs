using MediatR;
using OneOf;
using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Application.Features.Product.Commands.UpdateProduct
{
    /// <summary>
    /// Command to update an existing product.
    /// </summary>
    public class UpdateProductCommand : IRequest<OneOf<Unit, DomainValidationException>>
    {
        /// <summary>
        /// Identifier of the product to update.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the product.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description of the product.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Price of the product.
        /// </summary>
        public decimal Price { get; set; }

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
    }
}

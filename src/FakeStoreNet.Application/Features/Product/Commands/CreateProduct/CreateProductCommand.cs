using MediatR;
using OneOf;
using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Command to create a new product.
    /// </summary>
    public class CreateProductCommand : IRequest<OneOf<int, DomainValidationException>>
    {
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
        /// Rating average.
        /// </summary>
        public double Rate { get; set; }

        /// <summary>
        /// Rating count.
        /// </summary>
        public int Count { get; set; }
    }
}

using MediatR;

namespace FakeStoreNet.Application.Features.Product.Queries.GetProductById
{
    /// <summary>
    /// Query to retrieve a single product by its identifier.
    /// </summary>
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        /// <summary>
        /// Identifier of the product to retrieve.
        /// </summary>
        public int Id { get; set; }
    }
}

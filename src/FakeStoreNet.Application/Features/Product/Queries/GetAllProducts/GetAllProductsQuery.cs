using MediatR;

namespace FakeStoreNet.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Query to retrieve all products.
    /// </summary>
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }
}

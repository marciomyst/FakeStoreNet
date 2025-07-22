using MediatR;
using FakeStoreNet.Application.Common;

namespace FakeStoreNet.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Query to retrieve products with pagination and filtering.
    /// </summary>
    public class GetAllProductsQuery : IRequest<PagedResult<ProductDto>>
    {
        /// <summary>
        /// Gets or sets the page number (1-based). Defaults to 1.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size. Must be between 1 and 100. Defaults to 10.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the category filter.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Gets or sets the minimum price filter.
        /// </summary>
        public decimal? MinPrice { get; set; }

        /// <summary>
        /// Gets or sets the maximum price filter.
        /// </summary>
        public decimal? MaxPrice { get; set; }

        /// <summary>
        /// Gets or sets the search term to match title or description.
        /// </summary>
        public string? SearchTerm { get; set; }
    }
}

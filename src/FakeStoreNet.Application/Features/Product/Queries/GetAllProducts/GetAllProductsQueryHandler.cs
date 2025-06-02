using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using FakeStoreNet.Application.Features.Product.Queries.GetAllProducts;
using FakeStoreNet.Application.Features.Product.Queries;
using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Handles <see cref="GetAllProductsQuery"/> to retrieve all products.
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="GetAllProductsQueryHandler"/>.
        /// </summary>
        /// <param name="repository">Product repository.</param>
        /// <param name="mapper">AutoMapper mapper.</param>
        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Task.FromResult(dtos);
        }
    }
}

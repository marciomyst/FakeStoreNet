using AutoMapper;
using FakeStoreNet.Domain.Common;
using MediatR;

namespace FakeStoreNet.Application.Features.Product.Queries.GetProductById
{
    /// <summary>
    /// Handles <see cref="GetProductByIdQuery"/> to retrieve a single product.
    /// </summary>
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="GetProductByIdQueryHandler"/>.
        /// </summary>
        /// <param name="repository">Product repository.</param>
        /// <param name="mapper">AutoMapper mapper.</param>
        public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _repository.GetById(request.Id);
            var dto = _mapper.Map<ProductDto>(product);
            return Task.FromResult(dto);
        }
    }
}

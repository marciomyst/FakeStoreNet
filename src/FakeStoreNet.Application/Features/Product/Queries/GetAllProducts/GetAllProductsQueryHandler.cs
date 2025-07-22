using AutoMapper;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Domain.Common;
using MediatR;
using Microsoft.Extensions.Options;

namespace FakeStoreNet.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Handles <see cref="GetAllProductsQuery"/> to retrieve all products.
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly CacheSettings _cacheSettings;

        /// <summary>
        /// Initializes a new instance of <see cref="GetAllProductsQueryHandler"/>.
        /// </summary>
        /// <param name="repository">Product repository.</param>
        /// <param name="mapper">AutoMapper mapper.</param>
        /// <param name="cacheService">Cache service.</param>
        /// <param name="cacheSettings">Cache settings.</param>
        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper, ICacheService cacheService, IOptions<CacheSettings> cacheSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
            _cacheSettings = cacheSettings.Value;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _repository.GetAll();
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            var cacheKey = "GetAllProducts";
            var expiration = TimeSpan.FromSeconds(_cacheSettings.GetAllProductsAbsoluteExpirationInSeconds);
            await _cacheService.SetAsync(cacheKey, dtos, absoluteExpiration: expiration);
            return dtos;
        }
    }
}

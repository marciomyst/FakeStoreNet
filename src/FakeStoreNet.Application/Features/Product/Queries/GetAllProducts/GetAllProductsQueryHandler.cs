using AutoMapper;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Domain.Common;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FakeStoreNet.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Handles <see cref="GetAllProductsQuery"/> to retrieve products with pagination and filtering.
    /// </summary>
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PagedResult<ProductDto>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        private readonly CacheSettings _cacheSettings;

        public GetAllProductsQueryHandler(
            IProductRepository repository,
            IMapper mapper,
            ICacheService cacheService,
            IOptions<CacheSettings> cacheSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            // Retrieve all products
            var allProducts = _repository.GetAll();

            // Apply filters
            var queryable = allProducts.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Category))
                queryable = queryable.Where(p => p.Category == request.Category);
            if (request.MinPrice.HasValue)
                queryable = queryable.Where(p => p.Price.Amount >= request.MinPrice.Value);
            if (request.MaxPrice.HasValue)
                queryable = queryable.Where(p => p.Price.Amount <= request.MaxPrice.Value);
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.ToLower();
                queryable = queryable.Where(p =>
                    p.Title.ToLower().Contains(term) ||
                    p.Description.ToLower().Contains(term));
            }

            // Compute pagination
            var totalItems = queryable.Count();
            var skip = (request.Page - 1) * request.PageSize;
            var pageItems = queryable.Skip(skip).Take(request.PageSize).ToList();

            // Map to DTOs
            var dtos = _mapper.Map<IEnumerable<ProductDto>>(pageItems);

            // Cache this page
            var cacheKey = $"GetAllProducts_Page:{request.Page}_PageSize:{request.PageSize}_Category:{request.Category ?? "null"}_MinPrice:{request.MinPrice?.ToString() ?? "null"}_MaxPrice:{request.MaxPrice?.ToString() ?? "null"}_SearchTerm:{request.SearchTerm ?? "null"}";
            var expiration = TimeSpan.FromSeconds(_cacheSettings.GetAllProductsAbsoluteExpirationInSeconds);
            await _cacheService.SetAsync(cacheKey, dtos, absoluteExpiration: expiration);

            // Return paged result
            return new PagedResult<ProductDto>(dtos, totalItems, request.Page, request.PageSize);
        }
    }
}

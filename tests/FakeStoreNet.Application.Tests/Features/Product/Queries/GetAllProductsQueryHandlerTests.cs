using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using NSubstitute;
using Shouldly;
using Xunit;
using FakeStoreNet.Application.Features.Product.Queries.GetAllProducts;
using FakeStoreNet.Application.Features.Product.Queries;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Entities;
using DomainProduct = FakeStoreNet.Domain.Entities.Product;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Application.Tests.Features.Product.Queries
{
    public class GetAllProductsQueryHandlerTests
    {
        private readonly Faker _faker = new Faker();
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact(DisplayName = "Given repository returns products When handling query Then returns matching ProductDto list")]
        public async Task GivenRepositoryReturnsProducts_WhenHandlingQuery_ThenReturnsDtoList()
        {
            // Arrange
            var products = new List<DomainProduct>();
            for (int i = 1; i <= 3; i++)
            {
                var p = new DomainProduct(
                    _faker.Commerce.ProductName(),
                    new Money(_faker.Random.Decimal(1, 100), "USD"),
                    _faker.Commerce.ProductDescription(),
                    _faker.Commerce.Categories(1)[0],
                    _faker.Image.PicsumUrl(),
                    new Rating(_faker.Random.Double(0, 5), _faker.Random.Int(0, 1000))
                );
                p.GetType().GetProperty("Id")!.SetValue(p, i);
                products.Add(p);
            }
            _repository.GetAll().Returns(products);

            var query = new GetAllProductsQuery();
            var handler = new GetAllProductsQueryHandler(_repository, _mapper);

            // Act
            var dtos = await handler.Handle(query, CancellationToken.None);

            // Assert
            dtos.ShouldNotBeNull();
            dtos.ShouldNotBeEmpty(); // at least one projection works
            dtos.ShouldBeEquivalentTo(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [Fact(DisplayName = "Given repository returns empty When handling query Then returns empty list")]
        public async Task GivenRepositoryReturnsEmpty_WhenHandlingQuery_ThenReturnsEmptyList()
        {
            // Arrange
            _repository.GetAll().Returns(new List<DomainProduct>());

            var query = new GetAllProductsQuery();
            var handler = new GetAllProductsQueryHandler(_repository, _mapper);

            // Act
            var dtos = await handler.Handle(query, CancellationToken.None);

            // Assert
            dtos.ShouldBeEmpty();
        }
    }
}

using AutoMapper;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Application.Features.Product.Queries;
using FakeStoreNet.Application.Features.Product.Queries.GetAllProducts;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using Microsoft.Extensions.Options;
using NSubstitute;
using DomainProduct = FakeStoreNet.Domain.Entities.Product;

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
            var cacheService = Substitute.For<ICacheService>();
            var cacheSettings = Options.Create(new CacheSettings { GetAllProductsAbsoluteExpirationInSeconds = 60 });
            var handler = new GetAllProductsQueryHandler(_repository, _mapper, cacheService, cacheSettings);

            // Act
            var dtos = await handler.Handle(query, CancellationToken.None);


            // Assert
            dtos.ShouldNotBeNull();
            dtos.Items.ShouldNotBeEmpty(); // at least one projection works
            dtos.Items.Count().ShouldBe(3);
        }

        [Fact(DisplayName = "Given repository returns empty When handling query Then returns empty list")]
        public async Task GivenRepositoryReturnsEmpty_WhenHandlingQuery_ThenReturnsEmptyList()
        {
            // Arrange
            _repository.GetAll().Returns(new List<DomainProduct>());

            var query = new GetAllProductsQuery();
            var cacheService = Substitute.For<ICacheService>();
            var cacheSettings = Options.Create(new CacheSettings { GetAllProductsAbsoluteExpirationInSeconds = 60 });
            var handler = new GetAllProductsQueryHandler(_repository, _mapper, cacheService, cacheSettings);

            // Act
            var dtos = await handler.Handle(query, CancellationToken.None);

            // Assert
            dtos.ShouldBeEmpty();
        }
    }
}

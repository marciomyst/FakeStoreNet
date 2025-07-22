using AutoMapper;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Application.Features.Product.Queries.GetProductById;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using NSubstitute;
using DomainProduct = FakeStoreNet.Domain.Entities.Product;

namespace FakeStoreNet.Application.Tests.Features.Product.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Faker _faker = new();
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandlerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact(DisplayName = "Given existing product When handling query Then returns matching ProductDto")]
        public async Task GivenExistingProduct_WhenHandlingQuery_ThenReturnsProductDto()
        {
            // Arrange
            var product = new DomainProduct(
                _faker.Commerce.ProductName(),
                new Money(15.5m, "USD"),
                _faker.Commerce.ProductDescription(),
                _faker.Commerce.Categories(1)[0],
                _faker.Image.PicsumUrl(),
                new Rating(_faker.Random.Double(0, 5), _faker.Random.Int(0, 1000)));
            product.GetType().GetProperty("Id")!.SetValue(product, 10);

            _repository.GetById(10).Returns(product);
            var query = new GetProductByIdQuery { Id = 10 };
            var handler = new GetProductByIdQueryHandler(_repository, _mapper);

            // Act
            var dto = await handler.Handle(query, CancellationToken.None);

            // Assert
            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(10);
            dto.Title.ShouldBe(product.Title);
            dto.Price.ShouldBe(product.Price.Amount);
            dto.Category.ShouldBe(product.Category);
            dto.Rate.ShouldBe(product.Rating.Rate);
            dto.Count.ShouldBe(product.Rating.Count);
        }

        [Fact(DisplayName = "Given repository throws not found When handling query Then exception bubbles up")]
        public async Task GivenRepositoryThrowsError_WhenHandlingQuery_ThenThrows()
        {
            // Arrange
            _repository
                .When(r => r.GetById(999))
                .Do(call => throw new DomainValidationException("Not found"));
            var query = new GetProductByIdQuery { Id = 999 };
            var handler = new GetProductByIdQueryHandler(_repository, _mapper);

            // Act & Assert
            await Should.ThrowAsync<DomainValidationException>(() =>
                handler.Handle(query, CancellationToken.None));
        }
    }
}

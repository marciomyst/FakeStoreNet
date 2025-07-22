using FakeStoreNet.Application.Common;
using FakeStoreNet.Application.Features.Product.Commands.UpdateProduct;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using NSubstitute;
using DomainProduct = FakeStoreNet.Domain.Entities.Product;

namespace FakeStoreNet.Application.Tests.Features.Product.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Faker _faker = new Faker();
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();

        [Fact(DisplayName = "Given valid update command When handling Then updates and returns Unit")]
        public async Task GivenValidUpdateCommand_WhenHandling_ThenReturnsUnit()
        {
            // Arrange
            var existing = new DomainProduct(
                _faker.Commerce.ProductName(),
                new Money(10m, "USD"),
                _faker.Commerce.ProductDescription(),
                _faker.Commerce.Categories(1)[0],
                _faker.Image.PicsumUrl(),
                new Rating(4.5, 100));
            existing.GetType().GetProperty("Id")!.SetValue(existing, 1);

            _repository.GetById(1).Returns(existing);

            var command = new UpdateProductCommand
            {
                Id = 1,
                Title = "New Title",
                Description = "New Desc",
                Price = 20m,
                Category = "NewCat",
                Image = "http://example.com/new.png",
                Rate = 3.3,
                Count = 50
            };

            var cacheService = Substitute.For<ICacheService>();
            var handler = new UpdateProductCommandHandler(_repository, cacheService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsT0.ShouldBeTrue();
            existing.Title.ShouldBe("New Title");
            existing.Price.Amount.ShouldBe(20m);
        }

        [Fact(DisplayName = "Given invalid update command When handling Then returns DomainValidationException")]
        public async Task GivenInvalidUpdateCommand_WhenHandling_ThenReturnsDomainValidationException()
        {
            // Arrange
            var existing = new DomainProduct(
                "Title", new Money(5m, "USD"), "Desc", "Cat", "http://img", new Rating(2, 5));
            existing.GetType().GetProperty("Id")!.SetValue(existing, 2);
            _repository.GetById(2).Returns(existing);

            var command = new UpdateProductCommand
            {
                Id = 2,
                Title = "", // invalid
                Description = "Desc",
                Price = -1m,
                Category = "",
                Image = "bad",
                Rate = -1,
                Count = -1
            };

            var cacheService = Substitute.For<ICacheService>();
            var handler = new UpdateProductCommandHandler(_repository, cacheService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsT1.ShouldBeTrue();
            result.AsT1.Message.ShouldContain("required");
        }
    }
}

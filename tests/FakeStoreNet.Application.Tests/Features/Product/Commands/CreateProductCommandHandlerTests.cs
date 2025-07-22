using FakeStoreNet.Application.Common;
using FakeStoreNet.Application.Features.Product.Commands.CreateProduct;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Exceptions;
using NSubstitute;

namespace FakeStoreNet.Application.Tests.Features.Product.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Faker _faker = new Faker();
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();
        private readonly CreateProductCommandValidator _validator = new CreateProductCommandValidator();

        [Fact(DisplayName = "Given valid command When handling Then returns new product id")]
        public async Task GivenValidCommand_WhenHandling_ThenReturnsNewProductId()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = decimal.Parse(_faker.Commerce.Price()),
                Category = _faker.Commerce.Categories(1)[0],
                Image = _faker.Image.PicsumUrl(),
                Rate = _faker.Random.Double(0.0, 5.0),
                Count = _faker.Random.Int(0, 1000)
            };

            int capturedId = 0;
            _repository
                .When(r => r.Add(Arg.Any<Domain.Entities.Product>()))
                .Do(callInfo =>
                {
                    var product = callInfo.Arg<Domain.Entities.Product>();
                    // Simulate database assigning Id = 42
                    product.GetType().GetProperty("Id")!.SetValue(product, 42);
                    capturedId = product.Id;
                });

            var cacheService = Substitute.For<ICacheService>();
            var handler = new CreateProductCommandHandler(_repository, cacheService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsT0.ShouldBeTrue();
            result.AsT0.ShouldBe(42);
            capturedId.ShouldBe(42);
        }

        [Fact(DisplayName = "Given invalid command When validating Then fails validation")]
        public void GivenInvalidCommand_WhenValidating_ThenFailsValidation()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = "", // invalid
                Description = "",
                Price = -1,
                Category = "",
                Image = "not-a-url",
                Rate = -1,
                Count = -5
            };

            // Act
            var validation = _validator.Validate(command);

            // Assert
            validation.IsValid.ShouldBeFalse();
            validation.Errors.ShouldNotBeEmpty();
        }

        [Fact(DisplayName = "Given command with domain error When handling Then returns DomainValidationException")]
        public async Task GivenCommandWithDomainError_WhenHandling_ThenReturnsDomainValidationException()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Title = "Valid Title",
                Description = "Desc",
                Price = 10,
                Category = "Cat",
                Image = "http://example.com/image.png",
                Rate = 2.5,
                Count = 10
            };

            // Simulate repository.Add throwing DomainValidationException
            _repository
                .When(r => r.Add(Arg.Any<Domain.Entities.Product>()))
                .Do(callInfo => throw new DomainValidationException("Test error"));

            var cacheService = Substitute.For<ICacheService>();
            var handler = new CreateProductCommandHandler(_repository, cacheService);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsT1.ShouldBeTrue();
            result.AsT1.Message.ShouldBe("Test error");
        }
    }
}

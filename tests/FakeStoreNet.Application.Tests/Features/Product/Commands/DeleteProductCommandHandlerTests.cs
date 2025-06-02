using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using OneOf;
using Shouldly;
using Xunit;
using FakeStoreNet.Application.Features.Product.Commands.DeleteProduct;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Entities;
using DomainProduct = FakeStoreNet.Domain.Entities.Product;

namespace FakeStoreNet.Application.Tests.Features.Product.Commands
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly IProductRepository _repository = Substitute.For<IProductRepository>();

        [Fact(DisplayName = "Given valid delete command When handling Then returns Unit")]
        public async Task GivenValidDeleteCommand_WhenHandling_ThenReturnsUnit()
        {
            // Arrange
            var existing = new DomainProduct("T", new Domain.ValueObjects.Money(1m, "USD"), "D", "C", "I", new Domain.ValueObjects.Rating(1, 1));
            existing.GetType().GetProperty("Id")!.SetValue(existing, 3);
            _repository.GetById(3).Returns(existing);

            var command = new DeleteProductCommand { Id = 3 };
            var handler = new DeleteProductCommandHandler(_repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsT0.ShouldBeTrue();
            _repository.Received(1).Delete(existing);
        }

        [Fact(DisplayName = "Given repository throws domain error When handling Then returns DomainValidationException")]
        public async Task GivenRepositoryThrowsError_WhenHandling_ThenReturnsDomainValidationException()
        {
            // Arrange
            var existing = new DomainProduct("T", new Domain.ValueObjects.Money(1m, "USD"), "D", "C", "I", new Domain.ValueObjects.Rating(1, 1));
            existing.GetType().GetProperty("Id")!.SetValue(existing, 4);
            _repository.GetById(4).Returns(existing);
            _repository
                .When(r => r.Delete(existing))
                .Do(call => throw new DomainValidationException("Delete error"));

            var command = new DeleteProductCommand { Id = 4 };
            var handler = new DeleteProductCommandHandler(_repository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsT1.ShouldBeTrue();
            result.AsT1.Message.ShouldBe("Delete error");
        }
    }
}

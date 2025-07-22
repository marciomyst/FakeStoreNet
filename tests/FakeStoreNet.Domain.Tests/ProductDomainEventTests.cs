using FakeStoreNet.Domain.Common.Events;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Tests
{
    /// <summary>
    /// Tests for domain event enqueuing in the Product aggregate.
    /// </summary>
    public class ProductDomainEventTests
    {
        private readonly Money ValidPrice = new Money(10m, "USD");
        private readonly Rating ValidRating = new Rating(4.5, 10);

        [Fact(DisplayName = "Given a new Product, when constructed, then a ProductCreatedEvent is enqueued")]
        public void GivenNewProduct_WhenConstructed_ThenDomainEventEnqueued()
        {
            // Given & When
            var product = new Product("Title", ValidPrice, "Desc", "Cat", "Img", ValidRating);

            // Then
            product.DomainEvents.ShouldNotBeEmpty();
            var evt = product.DomainEvents.First() as ProductCreatedEvent;
            evt.ShouldNotBeNull();
            evt.AggregateId.ShouldBe(product.Id);
            evt.ProductId.ShouldBe(product.Id);
            evt.Title.ShouldBe(product.Title);
            evt.Price.ShouldBe(product.Price.Amount);
            evt.Category.ShouldBe(product.Category);
        }
    }
}

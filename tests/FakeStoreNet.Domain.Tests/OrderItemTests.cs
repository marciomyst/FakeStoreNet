using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Tests
{
    public class OrderItemTests
    {
        [Fact(DisplayName = "Given valid productId and quantity, when creating OrderItem, then properties are assigned")]
        public void GivenValidProductIdAndQuantity_WhenCreatingOrderItem_ThenPropertiesAreAssigned()
        {
            // Given
            int productId = 1;
            var qty = new Quantity(2);

            // When
            var orderItem = new OrderItem(productId, qty);

            // Then
            orderItem.ProductId.ShouldBe(productId);
            orderItem.Quantity.ShouldBe(qty);
        }

        [Fact(DisplayName = "Given invalid productId, when creating OrderItem, then DomainValidationException is thrown")]
        public void GivenInvalidProductId_WhenCreatingOrderItem_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new OrderItem(0, new Quantity(1)))
                  .Message.ShouldBe("ProductId must be greater than zero");
        }

        [Fact(DisplayName = "Given null quantity, when creating OrderItem, then DomainValidationException is thrown")]
        public void GivenNullQuantity_WhenCreatingOrderItem_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new OrderItem(1, null!))
                  .Message.ShouldBe("Quantity is required");
        }

        [Fact(DisplayName = "Given valid new quantity, when updating OrderItem, then quantity is updated")]
        public void GivenValidNewQuantity_WhenUpdatingOrderItem_ThenQuantityIsUpdated()
        {
            // Given
            var orderItem = new OrderItem(1, new Quantity(2));

            // When
            orderItem.UpdateQuantity(5);

            // Then
            orderItem.Quantity.Value.ShouldBe(5);
        }

        [Fact(DisplayName = "Given invalid new quantity, when updating OrderItem, then DomainValidationException is thrown")]
        public void GivenInvalidNewQuantity_WhenUpdatingOrderItem_ThenDomainValidationExceptionIsThrown()
        {
            var orderItem = new OrderItem(1, new Quantity(2));
            Should.Throw<DomainValidationException>(() => orderItem.UpdateQuantity(0))
                  .Message.ShouldBe("Quantity must be >= 1");
        }
    }
}

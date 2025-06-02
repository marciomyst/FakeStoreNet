using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Entities;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class OrderTests
    {
        private readonly OrderItem _validItem = new OrderItem(1, new ValueObjects.Quantity(2));

        [Fact(DisplayName = "Given valid parameters, when creating Order, then properties are initialized")]
        public void GivenValidParameters_WhenCreatingOrder_ThenPropertiesAreInitialized()
        {
            // Given
            int userId = 10;
            var items = new List<OrderItem> { _validItem };

            // When
            var order = new Order(userId, items);

            // Then
            order.UserId.ShouldBe(userId);
            order.Items.ShouldBe(items);
            order.IsSubmitted.ShouldBeFalse();
        }

        [Fact(DisplayName = "Given invalid userId, when creating Order, then DomainValidationException is thrown")]
        public void GivenInvalidUserId_WhenCreatingOrder_ThenExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Order(0, new List<OrderItem> { _validItem }))
                  .Message.ShouldBe("UserId must be greater than zero");
        }

        [Fact(DisplayName = "Given null items, when creating Order, then DomainValidationException is thrown")]
        public void GivenNullItems_WhenCreatingOrder_ThenExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Order(1, null!))
                  .Message.ShouldBe("Items are required");
        }

        [Fact(DisplayName = "Given empty items list, when creating Order, then DomainValidationException is thrown")]
        public void GivenEmptyItems_WhenCreatingOrder_ThenExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Order(1, new List<OrderItem>()))
                  .Message.ShouldBe("At least one order item is required");
        }

        [Fact(DisplayName = "Given valid order, when submitting Order, then IsSubmitted is true")]
        public void GivenValidOrder_WhenSubmittingOrder_ThenIsSubmittedIsTrue()
        {
            var order = new Order(1, new List<OrderItem> { _validItem });

            order.SubmitOrder();

            order.IsSubmitted.ShouldBeTrue();
        }

        [Fact(DisplayName = "Given already submitted order, when submitting again, then DomainValidationException is thrown")]
        public void GivenAlreadySubmittedOrder_WhenSubmittingAgain_ThenExceptionIsThrown()
        {
            var order = new Order(1, new List<OrderItem> { _validItem });
            order.SubmitOrder();

            Should.Throw<DomainValidationException>(() => order.SubmitOrder())
                  .Message.ShouldBe("Order is already submitted");
        }
    }
}

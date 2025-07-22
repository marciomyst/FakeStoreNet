using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;

namespace FakeStoreNet.Domain.Tests
{
    public class CartTests
    {
        [Fact(DisplayName = "Given valid userId, when creating Cart, then properties are initialized")]
        public void GivenValidUserId_WhenCreatingCart_ThenPropertiesAreInitialized()
        {
            // Given
            int userId = 5;

            // When
            var cart = new Cart(userId);

            // Then
            cart.UserId.ShouldBe(userId);
            cart.Items.Count.ShouldBe(0);
        }

        [Fact(DisplayName = "Given invalid userId, when creating Cart, then DomainValidationException is thrown")]
        public void GivenInvalidUserId_WhenCreatingCart_ThenExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() => new Cart(0))
                  .Message.ShouldBe("UserId must be greater than zero");
        }

        [Fact(DisplayName = "Given a new item, when adding to cart, then item is added")]
        public void GivenNewItem_WhenAddingToCart_ThenItemIsAdded()
        {
            // Given
            var cart = new Cart(1);

            // When
            cart.AddItem(10, 2);

            // Then
            cart.Items.Count.ShouldBe(1);
            var item = cart.Items.First(i => i.ProductId == 10);
            item.Quantity.Value.ShouldBe(2);
        }

        [Fact(DisplayName = "Given existing item, when adding same product, then quantity is updated")]
        public void GivenExistingItem_WhenAddingSameProduct_ThenQuantityIsUpdated()
        {
            // Given
            var cart = new Cart(1);
            cart.AddItem(10, 2);

            // When
            cart.AddItem(10, 3);

            // Then
            cart.Items.Count.ShouldBe(1);
            var item = cart.Items.First(i => i.ProductId == 10);
            item.Quantity.Value.ShouldBe(5);
        }

        [Fact(DisplayName = "Given items, when removing an item, then item is removed")]
        public void GivenItems_WhenRemovingItem_ThenItemIsRemoved()
        {
            // Given
            var cart = new Cart(1);
            cart.AddItem(10, 1);
            cart.AddItem(20, 1);

            // When
            cart.RemoveItem(10);

            // Then
            cart.Items.ShouldNotContain(i => i.ProductId == 10);
            cart.Items.Count.ShouldBe(1);
        }

        [Fact(DisplayName = "Given removing non-existing item, when removing, then items remain unchanged")]
        public void GivenRemovingNonExistingItem_WhenRemoving_ThenItemsRemain()
        {
            // Given
            var cart = new Cart(1);
            cart.AddItem(10, 1);

            // When
            cart.RemoveItem(99);

            // Then
            cart.Items.Count.ShouldBe(1);
        }
    }
}

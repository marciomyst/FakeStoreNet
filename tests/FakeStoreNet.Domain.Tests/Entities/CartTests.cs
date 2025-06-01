using System;
using System.Linq;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.Entities
{
    public class CartTests
    {
        [Fact(DisplayName = "Ctor with valid id, userId and date sets properties")]
        public void Ctor_WithValidValues_SetsProperties()
        {
            // Given
            int id = 1;
            int userId = 2;
            var date = DateTime.UtcNow;

            // When
            var cart = new Cart(id, userId, date);

            // Then
            cart.Id.ShouldBe(id);
            cart.UserId.ShouldBe(userId);
            cart.Date.ShouldBe(date);
            cart.Items.ShouldBeEmpty();
        }

        [Theory(DisplayName = "Ctor with invalid id throws DomainValidationException")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Ctor_InvalidId_ThrowsDomainValidationException(int invalidId)
        {
            // Given
            int userId = 1;
            var date = DateTime.UtcNow;

            // When
            Action act = () => new Cart(invalidId, userId, date);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Id must be positive");
        }

        [Theory(DisplayName = "Ctor with invalid userId throws DomainValidationException")]
        [InlineData(0)]
        [InlineData(-5)]
        public void Ctor_InvalidUserId_ThrowsDomainValidationException(int invalidUserId)
        {
            // Given
            int id = 1;
            var date = DateTime.UtcNow;

            // When
            Action act = () => new Cart(id, invalidUserId, date);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("UserId must be positive");
        }

        [Fact(DisplayName = "Ctor with default date throws DomainValidationException")]
        public void Ctor_DefaultDate_ThrowsDomainValidationException()
        {
            // Given
            int id = 1;
            int userId = 1;
            var date = default(DateTime);

            // When
            Action act = () => new Cart(id, userId, date);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Date is required");
        }

        [Fact(DisplayName = "AddItem with new productId adds new item")]
        public void AddItem_NewItem_AddsItem()
        {
            // Given
            var cart = new Cart(1, 1, DateTime.UtcNow);
            int productId = 10;
            int quantity = 3;

            // When
            cart.AddItem(productId, quantity);

            // Then
            cart.Items.Count.ShouldBe(1);
            var item = cart.Items.Single();
            item.ProductId.ShouldBe(productId);
            item.Quantity.Value.ShouldBe(quantity);
        }

        [Fact(DisplayName = "AddItem with existing productId accumulates quantity")]
        public void AddItem_ExistingItem_AccumulatesQuantity()
        {
            // Given
            var cart = new Cart(1, 1, DateTime.UtcNow);
            int productId = 5;
            cart.AddItem(productId, 2);

            // When
            cart.AddItem(productId, 4);

            // Then
            cart.Items.Count.ShouldBe(1);
            cart.Items.Single().Quantity.Value.ShouldBe(6);
        }

        [Theory(DisplayName = "AddItem with invalid quantity throws DomainValidationException")]
        [InlineData(0)]
        [InlineData(-2)]
        public void AddItem_InvalidQuantity_ThrowsDomainValidationException(int invalidQty)
        {
            // Given
            var cart = new Cart(1, 1, DateTime.UtcNow);

            // When
            Action act = () => cart.AddItem(1, invalidQty);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Quantity must be ≥ 1");
        }

        [Fact(DisplayName = "RemoveItem with existing productId removes item")]
        public void RemoveItem_ExistingItem_RemovesItem()
        {
            // Given
            var cart = new Cart(1, 1, DateTime.UtcNow);
            cart.AddItem(7, 1);

            // When
            cart.RemoveItem(7);

            // Then
            cart.Items.ShouldBeEmpty();
        }

        [Fact(DisplayName = "RemoveItem with non-existing productId throws DomainValidationException")]
        public void RemoveItem_NonExistingItem_ThrowsDomainValidationException()
        {
            // Given
            var cart = new Cart(1, 1, DateTime.UtcNow);

            // When
            Action act = () => cart.RemoveItem(99);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Item not found");
        }

        [Fact(DisplayName = "Equals returns true for same id")]
        public void Equals_SameId_ReturnsTrue()
        {
            // Given
            var a = new Cart(1, 1, DateTime.UtcNow);
            var b = new Cart(1, 2, DateTime.UtcNow.AddDays(1));

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different id")]
        public void Equals_DifferentId_ReturnsFalse()
        {
            // Given
            var a = new Cart(1, 1, DateTime.UtcNow);
            var b = new Cart(2, 1, DateTime.UtcNow);

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

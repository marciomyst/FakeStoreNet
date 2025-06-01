using System;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.Entities
{
    public class CartItemTests
    {
        [Fact(DisplayName = "Ctor with valid productId and quantity sets properties")]
        public void Ctor_WithValidProductIdAndQuantity_SetsProperties()
        {
            // Given
            int productId = 5;
            var quantity = new Quantity(2);

            // When
            var item = new CartItem(productId, quantity);

            // Then
            item.ProductId.ShouldBe(productId);
            item.Quantity.Value.ShouldBe(quantity.Value);
        }

        [Theory(DisplayName = "Ctor with invalid productId throws DomainValidationException")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Ctor_InvalidProductId_ThrowsDomainValidationException(int productId)
        {
            // Given
            var quantity = new Quantity(1);

            // When
            Action act = () => new CartItem(productId, quantity);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("ProductId must be positive");
        }

        [Fact(DisplayName = "Ctor with null quantity throws DomainValidationException")]
        public void Ctor_NullQuantity_ThrowsDomainValidationException()
        {
            // Given
            int productId = 1;
            Quantity quantity = null!;

            // When
            Action act = () => new CartItem(productId, quantity);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Quantity is required");
        }

        [Fact(DisplayName = "UpdateQuantity with valid new quantity sets property")]
        public void UpdateQuantity_ValidNewQuantity_SetsProperty()
        {
            // Given
            var item = new CartItem(1, new Quantity(2));
            var newQuantity = new Quantity(5);

            // When
            item.UpdateQuantity(newQuantity);

            // Then
            item.Quantity.Value.ShouldBe(newQuantity.Value);
        }

        [Fact(DisplayName = "UpdateQuantity with null throws DomainValidationException")]
        public void UpdateQuantity_Null_ThrowsDomainValidationException()
        {
            // Given
            var item = new CartItem(1, new Quantity(2));
            Quantity newQuantity = null!;

            // When
            Action act = () => item.UpdateQuantity(newQuantity);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Quantity is required");
        }

        [Fact(DisplayName = "Equals returns true for same productId")]
        public void Equals_SameProductId_ReturnsTrue()
        {
            // Given
            var a = new CartItem(1, new Quantity(2));
            var b = new CartItem(1, new Quantity(3));

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different productId")]
        public void Equals_DifferentProductId_ReturnsFalse()
        {
            // Given
            var a = new CartItem(1, new Quantity(2));
            var b = new CartItem(2, new Quantity(2));

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

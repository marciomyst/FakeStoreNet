using System;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Domain.Tests.Entities
{
    public class ProductTests
    {
        private readonly Money validMoney = new Money(10m, "USD");
        private readonly Rating validRating = new Rating(4.0, 2);

        [Fact(DisplayName = "Ctor with valid data sets all properties")]
        public void Ctor_WithValidData_SetsAllProperties()
        {
            // Given
            int id = 1;
            string title = "Title";
            var price = validMoney;
            string description = "Desc";
            string category = "Cat";
            string image = "Img";
            var rating = validRating;

            // When
            var product = new Product(id, title, price, description, category, image, rating);

            // Then
            product.Id.ShouldBe(id);
            product.Title.ShouldBe(title);
            product.Price.ShouldBe(price);
            product.Description.ShouldBe(description);
            product.Category.ShouldBe(category);
            product.Image.ShouldBe(image);
            product.Rating.ShouldBe(rating);
        }

        [Theory(DisplayName = "Ctor with invalid id throws DomainValidationException")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Ctor_InvalidId_ThrowsDomainValidationException(int invalidId)
        {
            // Given
            string title = "T";
            var price = validMoney;
            string description = "D";
            string category = "C";
            string image = "I";
            var rating = validRating;

            // When
            Action act = () => new Product(invalidId, title, price, description, category, image, rating);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Id must be positive");
        }

        [Fact(DisplayName = "Ctor with null price throws DomainValidationException")]
        public void Ctor_NullPrice_ThrowsDomainValidationException()
        {
            // Given
            int id = 1;
            string title = "T";
            Money price = null!;
            string description = "D";
            string category = "C";
            string image = "I";
            var rating = validRating;

            // When
            Action act = () => new Product(id, title, price, description, category, image, rating);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Price is required");
        }

        [Fact(DisplayName = "Ctor with null rating throws DomainValidationException")]
        public void Ctor_NullRating_ThrowsDomainValidationException()
        {
            // Given
            int id = 1;
            string title = "T";
            var price = validMoney;
            string description = "D";
            string category = "C";
            string image = "I";
            Rating rating = null!;

            // When
            Action act = () => new Product(id, title, price, description, category, image, rating);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("Rating is required");
        }

        [Fact(DisplayName = "AdjustPrice with valid new price updates the price")]
        public void AdjustPrice_WithValidNewPrice_UpdatesPrice()
        {
            // Given
            var product = new Product(1, "T", validMoney, "D", "C", "I", validRating);
            var newPrice = new Money(20m, "USD");

            // When
            product.AdjustPrice(newPrice);

            // Then
            product.Price.ShouldBe(newPrice);
        }

        [Fact(DisplayName = "AdjustPrice with null new price throws DomainValidationException")]
        public void AdjustPrice_NullNewPrice_ThrowsDomainValidationException()
        {
            // Given
            var product = new Product(1, "T", validMoney, "D", "C", "I", validRating);
            Money newPrice = null!;

            // When
            Action act = () => product.AdjustPrice(newPrice);

            // Then
            var ex = Should.Throw<DomainValidationException>(act);
            ex.Message.ShouldBe("New price is required");
        }

        [Fact(DisplayName = "Equals returns true for same id")]
        public void Equals_SameId_ReturnsTrue()
        {
            // Given
            var a = new Product(1, "T", validMoney, "D", "C", "I", validRating);
            var b = new Product(1, "X", validMoney, "Y", "Z", "W", validRating);

            // When & Then
            a.ShouldBe(b);
            (a == b).ShouldBeTrue();
        }

        [Fact(DisplayName = "Equals returns false for different id")]
        public void Equals_DifferentId_ReturnsFalse()
        {
            // Given
            var a = new Product(1, "T", validMoney, "D", "C", "I", validRating);
            var b = new Product(2, "T", validMoney, "D", "C", "I", validRating);

            // When & Then
            a.ShouldNotBe(b);
            (a != b).ShouldBeTrue();
        }
    }
}

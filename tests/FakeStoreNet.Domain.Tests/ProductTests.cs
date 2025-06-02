using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.ValueObjects;
using Shouldly;

namespace FakeStoreNet.Domain.Tests
{
    public class ProductTests
    {
        private readonly Money ValidPrice = new Money(10m, "USD");
        private readonly Rating ValidRating = new Rating(4.5, 10);

        [Fact(DisplayName = "Given valid parameters, when creating Product, then properties are assigned")]
        public void GivenValidParameters_WhenCreatingProduct_ThenPropertiesAreAssigned()
        {
            // Given
            var title = "Product A";
            var description = "Description";
            var category = "Category";
            var image = "http://image.url";

            // When
            var product = new Product(title, ValidPrice, description, category, image, ValidRating);

            // Then
            product.Title.ShouldBe(title);
            product.Price.ShouldBe(ValidPrice);
            product.Description.ShouldBe(description);
            product.Category.ShouldBe(category);
            product.Image.ShouldBe(image);
            product.Rating.ShouldBe(ValidRating);
        }

        [Fact(DisplayName = "Given null price, when creating Product, then DomainValidationException is thrown")]
        public void GivenNullPrice_WhenCreatingProduct_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() =>
                new Product("Title", null!, "Desc", "Cat", "Img", ValidRating))
                  .Message.ShouldBe("Price is required");
        }

        [Fact(DisplayName = "Given null rating, when creating Product, then DomainValidationException is thrown")]
        public void GivenNullRating_WhenCreatingProduct_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() =>
                new Product("Title", ValidPrice, "Desc", "Cat", "Img", null!))
                  .Message.ShouldBe("Rating is required");
        }

        [Fact(DisplayName = "Given empty title, when creating Product, then DomainValidationException is thrown")]
        public void GivenEmptyTitle_WhenCreatingProduct_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() =>
                new Product("", ValidPrice, "Desc", "Cat", "Img", ValidRating))
                  .Message.ShouldBe("Title is required");
        }

        [Fact(DisplayName = "Given empty category, when creating Product, then DomainValidationException is thrown")]
        public void GivenEmptyCategory_WhenCreatingProduct_ThenDomainValidationExceptionIsThrown()
        {
            Should.Throw<DomainValidationException>(() =>
                new Product("Title", ValidPrice, "Desc", "", "Img", ValidRating))
                  .Message.ShouldBe("Category is required");
        }

        [Fact(DisplayName = "Given valid new price, when adjusting price, then Price is updated")]
        public void GivenValidNewPrice_WhenAdjustingPrice_ThenPriceIsUpdated()
        {
            // Given
            var product = new Product("Title", ValidPrice, "Desc", "Cat", "Img", ValidRating);
            var newPrice = new Money(20m, "USD");

            // When
            product.AdjustPrice(newPrice);

            // Then
            product.Price.ShouldBe(newPrice);
        }

        [Fact(DisplayName = "Given null new price, when adjusting price, then DomainValidationException is thrown")]
        public void GivenNullNewPrice_WhenAdjustingPrice_ThenDomainValidationExceptionIsThrown()
        {
            var product = new Product("Title", ValidPrice, "Desc", "Cat", "Img", ValidRating);

            Should.Throw<DomainValidationException>(() =>
                product.AdjustPrice(null!))
                  .Message.ShouldBe("New price is required");
        }
    }
}

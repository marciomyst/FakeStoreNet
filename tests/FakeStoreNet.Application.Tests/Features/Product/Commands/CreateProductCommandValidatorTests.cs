using FluentValidation;
using FakeStoreNet.Application.Features.Product.Commands.CreateProduct;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Application.Tests.Features.Product.Commands
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator = new CreateProductCommandValidator();

        [Fact(DisplayName = "Given valid create command, validation succeeds")]
        public void GivenValidCreateCommand_ValidationSucceeds()
        {
            var command = new CreateProductCommand
            {
                Title = "Valid Title",
                Description = "Valid Description",
                Price = 10m,
                Category = "Category",
                Image = "http://example.com/image.png",
                Rate = 4.5,
                Count = 10
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Given invalid create command, validation fails with expected messages")]
        public void GivenInvalidCreateCommand_ValidationFails()
        {
            var command = new CreateProductCommand
            {
                Title = "",
                Description = "",
                Price = -5m,
                Category = "",
                Image = "not-a-url",
                Rate = -1.0,
                Count = -1
            };

            var result = _validator.Validate(command);

            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("required") || e.ErrorMessage.Contains("non-negative") || e.ErrorMessage.Contains("valid URL") || e.ErrorMessage.Contains("between 0.0 and 5.0"));
        }
    }
}

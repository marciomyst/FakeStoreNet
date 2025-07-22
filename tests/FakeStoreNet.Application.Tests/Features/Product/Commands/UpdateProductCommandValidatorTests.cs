using FluentValidation;
using FakeStoreNet.Application.Features.Product.Commands.UpdateProduct;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Application.Tests.Features.Product.Commands
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator _validator = new UpdateProductCommandValidator();

        [Fact(DisplayName = "Given valid update command, validation succeeds")]
        public void GivenValidUpdateCommand_ValidationSucceeds()
        {
            var command = new UpdateProductCommand
            {
                Id = 1,
                Title = "Valid Title",
                Description = "Valid Description",
                Price = 10m,
                Category = "Category",
                Image = "http://example.com/image.png",
                Rate = 4.0,
                Count = 5
            };

            var result = _validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Given invalid update command, validation fails with expected messages")]
        public void GivenInvalidUpdateCommand_ValidationFails()
        {
            var command = new UpdateProductCommand
            {
                Id = 0,
                Title = "",
                Description = "",
                Price = -1m,
                Category = "",
                Image = "not-a-url",
                Rate = -0.5,
                Count = -1
            };

            var result = _validator.Validate(command);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("greater than zero"));
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("required"));
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("non-negative"));
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("valid URL"));
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("between 0.0 and 5.0"));
        }
    }
}

using FluentValidation;
using FakeStoreNet.Application.Features.Product.Commands.DeleteProduct;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Application.Tests.Features.Product.Commands
{
    public class DeleteProductCommandValidatorTests
    {
        private readonly DeleteProductCommandValidator _validator = new DeleteProductCommandValidator();

        [Fact(DisplayName = "Given valid delete command, validation succeeds")]
        public void GivenValidDeleteCommand_ValidationSucceeds()
        {
            var command = new DeleteProductCommand
            {
                Id = 1
            };

            var result = _validator.Validate(command);
            result.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Given invalid delete command, validation fails with expected message")]
        public void GivenInvalidDeleteCommand_ValidationFails()
        {
            var command = new DeleteProductCommand
            {
                Id = 0
            };

            var result = _validator.Validate(command);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("greater than zero"));
        }
    }
}

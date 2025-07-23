using FluentValidation;
using FakeStoreNet.Application.Features.Product.Queries.GetProductById;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Application.Tests.Features.Product.Queries
{
    public class GetProductByIdQueryValidatorTests
    {
        private readonly GetProductByIdQueryValidator _validator = new GetProductByIdQueryValidator();

        [Fact(DisplayName = "Given valid id, validation succeeds")]
        public void GivenValidId_ValidationSucceeds()
        {
            var query = new GetProductByIdQuery { Id = 1 };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeTrue();
        }

        [Theory(DisplayName = "Given invalid id, validation fails")]
        [InlineData(0)]
        [InlineData(-5)]
        public void GivenInvalidId_ValidationFails(int invalidId)
        {
            var query = new GetProductByIdQuery { Id = invalidId };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("'id' must be greater than zero"));
        }
    }
}

using FluentValidation;
using FakeStoreNet.Application.Features.Product.Queries.GetAllProducts;
using Shouldly;
using Xunit;

namespace FakeStoreNet.Application.Tests.Features.Product.Queries
{
    public class GetAllProductsQueryValidatorTests
    {
        private readonly GetAllProductsQueryValidator _validator = new GetAllProductsQueryValidator();

        [Fact(DisplayName = "Given default query, validation succeeds")]
        public void GivenDefaultQuery_ValidationSucceeds()
        {
            var query = new GetAllProductsQuery();
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeTrue();
        }

        [Fact(DisplayName = "Given invalid page, validation fails")]
        public void GivenInvalidPage_ValidationFails()
        {
            var query = new GetAllProductsQuery { Page = 0 };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("'page' must be at least 1"));
        }

        [Fact(DisplayName = "Given invalid page size, validation fails")]
        public void GivenInvalidPageSize_ValidationFails()
        {
            var query = new GetAllProductsQuery { PageSize = 0 };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("'pageSize' must be between 1 and 100"));
        }

        [Fact(DisplayName = "Given negative min price, validation fails")]
        public void GivenNegativeMinPrice_ValidationFails()
        {
            var query = new GetAllProductsQuery { MinPrice = -1m };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("'minPrice' must be non-negative"));
        }

        [Fact(DisplayName = "Given negative max price, validation fails")]
        public void GivenNegativeMaxPrice_ValidationFails()
        {
            var query = new GetAllProductsQuery { MaxPrice = -5m };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("'maxPrice' must be non-negative"));
        }

        [Fact(DisplayName = "Given min price greater than max price, validation fails")]
        public void GivenMinPriceGreaterThanMaxPrice_ValidationFails()
        {
            var query = new GetAllProductsQuery { MinPrice = 10m, MaxPrice = 5m };
            var result = _validator.Validate(query);
            result.IsValid.ShouldBeFalse();
            result.Errors.ShouldContain(e => e.ErrorMessage.Contains("'minPrice' must be less than or equal to 'maxPrice'"));
        }
    }
}

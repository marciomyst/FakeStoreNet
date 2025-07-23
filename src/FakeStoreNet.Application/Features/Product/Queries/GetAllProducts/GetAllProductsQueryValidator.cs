using FluentValidation;

namespace FakeStoreNet.Application.Features.Product.Queries.GetAllProducts
{
    /// <summary>
    /// Validator for <see cref="GetAllProductsQuery"/>.
    /// </summary>
    public class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
    {
        public GetAllProductsQueryValidator()
        {
            RuleFor(q => q.Page)
                .GreaterThanOrEqualTo(1)
                .WithMessage("'page' must be at least 1.");

            RuleFor(q => q.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("'pageSize' must be between 1 and 100.");

            RuleFor(q => q.MinPrice.GetValueOrDefault())
                .GreaterThanOrEqualTo(0M)
                .WithMessage("'minPrice' must be non-negative.")
                .When(q => q.MinPrice.HasValue);

            RuleFor(q => q.MaxPrice.GetValueOrDefault())
                .GreaterThanOrEqualTo(0M)
                .WithMessage("'maxPrice' must be non-negative.")
                .When(q => q.MaxPrice.HasValue);

            When(q => q.MinPrice.HasValue && q.MaxPrice.HasValue, () =>
            {
                RuleFor(q => q)
                    .Must(q => q.MinPrice.GetValueOrDefault() <= q.MaxPrice.GetValueOrDefault())
                    .WithMessage("'minPrice' must be less than or equal to 'maxPrice'.");
            });
        }
    }
}

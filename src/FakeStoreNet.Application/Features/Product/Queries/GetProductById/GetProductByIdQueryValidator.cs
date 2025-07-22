using FluentValidation;

namespace FakeStoreNet.Application.Features.Product.Queries.GetProductById
{
    /// <summary>
    /// Validator for <see cref="GetProductByIdQuery"/>.
    /// </summary>
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .GreaterThan(0)
                .WithMessage("'id' must be greater than zero.");
        }
    }
}

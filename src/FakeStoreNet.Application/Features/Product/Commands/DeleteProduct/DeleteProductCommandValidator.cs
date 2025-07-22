using FluentValidation;

namespace FakeStoreNet.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Validator for <see cref="DeleteProductCommand"/>.
    /// </summary>
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DeleteProductCommandValidator"/>.
        /// </summary>
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");
        }
    }
}

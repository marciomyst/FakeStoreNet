using FluentValidation;

namespace FakeStoreNet.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Validator for <see cref="CreateProductCommand"/>.
    /// </summary>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CreateProductCommandValidator"/>.
        /// </summary>
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image URL is required.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Image must be a valid URL.");

            RuleFor(x => x.Rate)
                .InclusiveBetween(0.0, 5.0).WithMessage("Rate must be between 0.0 and 5.0.");

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Count must be zero or positive.");
        }
    }
}

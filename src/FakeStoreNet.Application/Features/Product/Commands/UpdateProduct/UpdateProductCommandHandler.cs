using FakeStoreNet.Application.Common;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using MediatR;
using OneOf;

namespace FakeStoreNet.Application.Features.Product.Commands.UpdateProduct
{
    /// <summary>
    /// Handles <see cref="UpdateProductCommand"/> to update an existing product.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of <see cref="UpdateProductCommandHandler"/>.
    /// </remarks>
    /// <param name="repository">Product repository.</param>
    /// <param name="cacheService">Cache service.</param>
    public class UpdateProductCommandHandler(IProductRepository repository, ICacheService cacheService) : IRequestHandler<UpdateProductCommand, OneOf<Unit, DomainValidationException>>
    {
        /// <inheritdoc/>
        public async Task<OneOf<Unit, DomainValidationException>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Title))
                    throw new DomainValidationException("Title is required");
                var product = repository.GetById(request.Id);
                product.UpdateDetails(
                    request.Title,
                    new Money(request.Price, product.Price.Currency),
                    request.Description,
                    request.Category,
                    request.Image,
                    new Rating(request.Rate, request.Count)
                );
                repository.Update(product);
                await cacheService.RemoveAsync("GetAllProducts");
                return Unit.Value;
            }
            catch (DomainValidationException ex)
            {
                return ex;
            }
        }
    }
}

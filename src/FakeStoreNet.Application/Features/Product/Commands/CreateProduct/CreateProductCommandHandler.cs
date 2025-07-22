using AutoMapper;
using FakeStoreNet.Application.Common;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;
using MediatR;
using OneOf;

namespace FakeStoreNet.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Handles <see cref="CreateProductCommand"/> to create a new product.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of <see cref="CreateProductCommandHandler"/>.
    /// </remarks>
    /// <param name="repository">Product repository.</param>
    public class CreateProductCommandHandler(IProductRepository repository, ICacheService cacheService) : IRequestHandler<CreateProductCommand, OneOf<int, DomainValidationException>>
    {
        /// <inheritdoc/>
        public async Task<OneOf<int, DomainValidationException>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Domain.Entities.Product(
                    request.Title,
                    new Money(request.Price, "USD"),
                    request.Description,
                    request.Category,
                    request.Image,
                    new Rating(request.Rate, request.Count));

                repository.Add(product);
                await cacheService.RemoveAsync("GetAllProducts");

                return product.Id;
            }
            catch (DomainValidationException ex)
            {
                return ex;
            }
        }
    }
}

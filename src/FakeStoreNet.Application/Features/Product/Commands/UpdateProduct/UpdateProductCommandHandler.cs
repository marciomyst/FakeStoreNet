using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OneOf;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Application.Features.Product.Commands.UpdateProduct;

namespace FakeStoreNet.Application.Features.Product.Commands.UpdateProduct
{
    /// <summary>
    /// Handles <see cref="UpdateProductCommand"/> to update an existing product.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, OneOf<Unit, DomainValidationException>>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateProductCommandHandler"/>.
        /// </summary>
        /// <param name="repository">Product repository.</param>
        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public Task<OneOf<Unit, DomainValidationException>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Title))
                    throw new DomainValidationException("Title is required");
                var product = _repository.GetById(request.Id);
                product.UpdateDetails(
                    request.Title,
new Money(request.Price, product.Price.Currency),
                    request.Description,
                    request.Category,
                    request.Image,
                    new Rating(request.Rate, request.Count)
                );
                _repository.Update(product);
                return Task.FromResult<OneOf<Unit, DomainValidationException>>(Unit.Value);
            }
            catch (DomainValidationException ex)
            {
                return Task.FromResult<OneOf<Unit, DomainValidationException>>(ex);
            }
        }
    }
}

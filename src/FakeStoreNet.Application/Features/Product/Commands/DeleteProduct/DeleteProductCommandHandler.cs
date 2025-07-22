using FakeStoreNet.Application.Common;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Exceptions;
using MediatR;
using OneOf;

namespace FakeStoreNet.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Handles <see cref="DeleteProductCommand"/> to delete an existing product.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of <see cref="DeleteProductCommandHandler"/>.
    /// </remarks>
    /// <param name="repository">Product repository.</param>
    public class DeleteProductCommandHandler(IProductRepository repository, ICacheService cacheService) : IRequestHandler<DeleteProductCommand, OneOf<Unit, DomainValidationException>>
    {

        /// <inheritdoc/>
        public async Task<OneOf<Unit, DomainValidationException>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = repository.GetById(request.Id);
                repository.Delete(product);
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

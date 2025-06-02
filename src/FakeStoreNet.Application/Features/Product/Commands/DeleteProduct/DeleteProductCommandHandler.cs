using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using FakeStoreNet.Domain.Common;

namespace FakeStoreNet.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Handles <see cref="DeleteProductCommand"/> to delete an existing product.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, OneOf<Unit, DomainValidationException>>
    {
        private readonly IProductRepository _repository;

        /// <summary>
        /// Initializes a new instance of <see cref="DeleteProductCommandHandler"/>.
        /// </summary>
        /// <param name="repository">Product repository.</param>
        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc/>
        public Task<OneOf<Unit, DomainValidationException>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _repository.GetById(request.Id);
                _repository.Delete(product);
                return Task.FromResult<OneOf<Unit, DomainValidationException>>(Unit.Value);
            }
            catch (DomainValidationException ex)
            {
                return Task.FromResult<OneOf<Unit, DomainValidationException>>(ex);
            }
        }
    }
}

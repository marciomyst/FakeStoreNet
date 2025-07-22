using FakeStoreNet.Domain.Exceptions;
using MediatR;
using OneOf;

namespace FakeStoreNet.Application.Features.Product.Commands.DeleteProduct
{
    /// <summary>
    /// Command to delete an existing product.
    /// </summary>
    public class DeleteProductCommand : IRequest<OneOf<Unit, DomainValidationException>>
    {
        /// <summary>
        /// Identifier of the product to delete.
        /// </summary>
        public int Id { get; set; }
    }
}

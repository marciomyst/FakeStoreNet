using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using OneOf;
using FakeStoreNet.Domain.Common;
using FakeStoreNet.Domain.Entities;
using FakeStoreNet.Domain.ValueObjects;
using FakeStoreNet.Application.Features.Product.Commands.CreateProduct;

namespace FakeStoreNet.Application.Features.Product.Commands.CreateProduct
{
    /// <summary>
    /// Handles <see cref="CreateProductCommand"/> to create a new product.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, OneOf<int, DomainValidationException>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="CreateProductCommandHandler"/>.
        /// </summary>
        /// <param name="repository">Product repository.</param>
        /// <param name="mapper">AutoMapper mapper.</param>
        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc/>
        public Task<OneOf<int, DomainValidationException>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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

                _repository.Add(product);

                return Task.FromResult<OneOf<int, DomainValidationException>>(product.Id);
            }
            catch (DomainValidationException ex)
            {
                return Task.FromResult<OneOf<int, DomainValidationException>>(ex);
            }
        }
    }
}

using System.Collections.Generic;
using FakeStoreNet.Domain.Entities;

namespace FakeStoreNet.Domain.Common
{
    /// <summary>
    /// Repository interface for Product aggregate.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Retrieves a product by its identifier or throws an <see cref="EntityNotFoundException"/>.
        /// </summary>
        /// <param name="id">Identifier of the product.</param>
        /// <returns>Product entity.</returns>
        Product GetById(int id);

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>Collection of product entities.</returns>
        IEnumerable<Product> GetAll();

        /// <summary>
        /// Persists a new product.
        /// </summary>
        /// <param name="product">Product entity to add.</param>
        void Add(Product product);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">Product entity with updated values.</param>
        void Update(Product product);

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="product">Product entity to delete.</param>
        void Delete(Product product);
    }
}

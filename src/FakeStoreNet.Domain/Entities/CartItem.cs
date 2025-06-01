using System;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Represents an item in a shopping cart.
    /// </summary>
    public sealed class CartItem : IEquatable<CartItem>
    {
        public int ProductId { get; }
        public Quantity Quantity { get; private set; }

        public CartItem(int productId, Quantity quantity)
        {
            if (productId <= 0)
                throw new DomainValidationException("ProductId must be positive");
            Quantity = quantity ?? throw new DomainValidationException("Quantity is required");
            ProductId = productId;
        }

        public void UpdateQuantity(Quantity newQuantity)
        {
            if (newQuantity is null)
                throw new DomainValidationException("Quantity is required");
            Quantity = newQuantity;
        }

        public bool Equals(CartItem? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ProductId == other.ProductId;
        }

        public override bool Equals(object? obj) => Equals(obj as CartItem);
        public override int GetHashCode() => ProductId.GetHashCode();
        public static bool operator ==(CartItem? left, CartItem? right) => Equals(left, right);
        public static bool operator !=(CartItem? left, CartItem? right) => !Equals(left, right);
    }
}

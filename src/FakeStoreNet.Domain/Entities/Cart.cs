using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FakeStoreNet.Domain.Exceptions;
using FakeStoreNet.Domain.ValueObjects;

namespace FakeStoreNet.Domain.Entities
{
    /// <summary>
    /// Primary entity representing a shopping cart.
    /// </summary>
    public sealed class Cart : IEquatable<Cart>
    {
        private readonly List<CartItem> _items = new();

        public int Id { get; }
        public int UserId { get; }
        public DateTime Date { get; }
        public IReadOnlyCollection<CartItem> Items => new ReadOnlyCollection<CartItem>(_items);

        public Cart(int id, int userId, DateTime date)
        {
            if (id <= 0)
                throw new DomainValidationException("Id must be positive");
            if (userId <= 0)
                throw new DomainValidationException("UserId must be positive");
            if (date == default)
                throw new DomainValidationException("Date is required");

            Id = id;
            UserId = userId;
            Date = date;
        }

        public void AddItem(int productId, int quantity)
        {
            var qty = new Quantity(quantity);
            if (qty is null)
                throw new DomainValidationException("Quantity is required");

            var existing = _items.SingleOrDefault(i => i.ProductId == productId);
            if (existing != null)
            {
                var updated = new Quantity(existing.Quantity.Value + quantity);
                existing.UpdateQuantity(updated);
            }
            else
            {
                _items.Add(new CartItem(productId, qty));
            }
        }

        public void RemoveItem(int productId)
        {
            var existing = _items.SingleOrDefault(i => i.ProductId == productId);
            if (existing == null)
                throw new DomainValidationException("Item not found");
            _items.Remove(existing);
        }

        public bool Equals(Cart? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object? obj) => Equals(obj as Cart);

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Cart? left, Cart? right) => Equals(left, right);

        public static bool operator !=(Cart? left, Cart? right) => !Equals(left, right);
    }
}

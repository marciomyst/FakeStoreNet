using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FakeStoreNet.Application.Common
{
    /// <summary>
    /// Represents a paged result with metadata.
    /// </summary>
    /// <typeparam name="T">The type of items.</typeparam>
    public class PagedResult<T> : IEnumerable<T>
    {
        /// <summary>
        /// The items for the current page.
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// The total number of items matching the query.
        /// </summary>
        public int TotalItems { get; }

        /// <summary>
        /// The total number of pages.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// The current page number.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// The page size.
        /// </summary>
        public int PageSize { get; }

        public PagedResult(IEnumerable<T> items, int totalItems, int currentPage, int pageSize)
        {
            Items = items;
            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = pageSize > 0
                ? (int)Math.Ceiling(totalItems / (double)pageSize)
                : 0;
        }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override bool Equals(object obj)
        {
            if (obj is PagedResult<T> other)
            {
                return Items.SequenceEqual(other.Items)
                       && TotalItems == other.TotalItems
                       && CurrentPage == other.CurrentPage
                       && PageSize == other.PageSize;
            }
            if (obj is IEnumerable<T> otherEnumerable)
            {
                return Items.SequenceEqual(otherEnumerable);
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                foreach (var item in Items)
                    hash = hash * 23 + (item?.GetHashCode() ?? 0);
                hash = hash * 23 + TotalItems.GetHashCode();
                hash = hash * 23 + CurrentPage.GetHashCode();
                hash = hash * 23 + PageSize.GetHashCode();
                return hash;
            }
        }
    }
}

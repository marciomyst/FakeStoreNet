using System;

namespace FakeStoreNet.Domain.Exceptions
{
    /// <summary>
    /// Exception thrown when a domain invariant is violated.
    /// </summary>
    public class DomainValidationException : Exception
    {
        public DomainValidationException(string message)
            : base(message)
        {
        }
    }
}
